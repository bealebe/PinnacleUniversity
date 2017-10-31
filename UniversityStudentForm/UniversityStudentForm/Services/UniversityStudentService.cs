using PinnacleUniversity.DataModels;
using PinnacleUniversity.REST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PinnacleUniversity.Services
{
    /// <summary>
    /// This class will serve the resutls from both the DB and the API Client class.
    /// </summary>
    public class UniversityStudentService : IUniversityStudentService
    {
        #region Constructor and Private Members

        private UniversityClient _Client;
        private UniversityContext _DB;

        public UniversityStudentService()
        {
            _Client = new UniversityClient();
            _DB = new UniversityContext();
        }

        #endregion Constructor and Private Members

        #region IUniviersityStudentService Implementation

        /// <summary>
        /// This method will initialize the entire database by reseting the API, getting the result, and then building the DB for Activity 1 of the assessment.
        /// </summary>
        /// <returns></returns>
        public bool Initialize(IProgress<string> pProgress)
        {
            bool success = false;

            try
            {
                pProgress.Report("Resetting Database and server Data");
                ResetDB();

                pProgress.Report("Getting all students from server");
                var students = _Client.GetAllStudents();

                pProgress.Report("Creating local data.");
                //Create DB of Students.
                foreach (StudentRoot s in students)
                {
                    var student = _Client.GetStudent(s.Id);

                    if (student.Id != s.Id)
                    {
                        //Error, take original data.
                        student.Id = s.Id;
                        student.Name = s.Name;
                        student.Email = s.Email;
                        //Try to rebuild the data from other calls to the API... Doesn't return any results so just add the original data.
                        // _DB.StudentsOverview.Add(RebuildStudentDetailsFromCourseRegistry(student));
                        _DB.StudentsOverview.Add(new StudentOverview(student));
                    }
                    else
                    {
                        _DB.StudentsOverview.Add(new StudentOverview(student));
                    }
                }
                _DB.SaveChanges();
            }
            catch (Exception e)
            {
                success = false;
                pProgress.Report(e.Message);
            }
            return success;
        }

        /// <summary>
        /// Initiates the the dropping course sequence for Activity 2.
        /// </summary>
        /// <param name="pStudents"></param>
        /// <returns></returns>
        public bool BeginDroppingStudents(BindingList<StudentOverview> pStudents, IProgress<string> pProgress)
        {
            bool success = false;
            try
            {
                int TotalCoursesDropped = 0;
                //Loop thru all of the students passed in from the View Model
                foreach (StudentOverview s in pStudents)
                {
                    //Sort the courses by grade
                    s.Courses.Sort((c1, c2) => c1.Grade.CompareTo(c2.Grade));
                    foreach (EnrolledCourse c in s.Courses)
                    {
                        // loop thru student courses to see if it needs to be dropped.
                        if (s.FullTime && c.Grade < 40f && s.ClassOkayToDrop(c))
                        {
                            try
                            {
                                Console.WriteLine("Dropping Course " + c.Title);
                                c.Status = EnrolledStudent.DROPPED;
                                s.CalculateDerivedProperties();
                                DropStudent(s.Id, c.CourseId);
                                UpdateStudent(s);
                                pProgress.Report(s.Name + " Dropped " + c.Title);
                            }
                            catch (IOException e)
                            {
                                pProgress.Report(e.Message);
                            }

                            TotalCoursesDropped++;
                        }
                    }
                }
                Console.WriteLine("Total Courses Dropped: " + TotalCoursesDropped);
                success = Validate();
            }
            catch (Exception e)
            {
                pProgress.Report(e.Message);
                success = false;
            }
            return success;
        }

        public void UpdateStudent(StudentOverview pStudent)
        {
            var student = _DB.StudentsOverview.Include("Courses")
                .Where(s => s.Id == pStudent.Id).FirstOrDefault<StudentOverview>();

            var courses = pStudent.Courses.Except(student.Courses, cours => cours.Status == EnrolledStudent.DROPPED).ToList<EnrolledCourse>();

            courses.ForEach(c => student.Courses.Remove(c));

            _DB.SaveChanges();
        }

        /// <summary>
        /// Get's all the student courses from the DB
        /// </summary>
        /// <param name="pStudentId"></param>
        /// <returns></returns>
        public List<EnrolledCourse> GetAllStudentCoursesFromDB(int pStudentId)
        {
            List<EnrolledCourse> list = null;

            var query = from s in _DB.EnrolledCourses
                        where s.Id == pStudentId
                        orderby s.Id
                        select s;
            list = query.ToList();

            return list;
        }

        /// <summary>
        /// Get all the students from the DB.
        /// </summary>
        /// <returns></returns>
        public BindingList<StudentOverview> GetAllStudentsFromDB()
        {
            BindingList<StudentOverview> list = null;

            var query = from s in _DB.StudentsOverview
                        orderby s.Id
                        select s;
            list = new BindingList<StudentOverview>(query.ToList());

            return list;
        }

        /// <summary>
        /// Delete the DB
        /// </summary>
        /// <returns></returns>
        public bool ResetDB()
        {
            _Client.ResetAPIData();

            _DB.Database.ExecuteSqlCommand("DELETE FROM StudentOverview");
            _DB.SaveChanges();

            return true;
        }

        /// <summary>
        /// This method will drop a student from a a course via the API client.
        /// </summary>
        /// <param name="pStudentId"></param>
        /// <param name="pCourseId"></param>
        /// <returns></returns>
        public bool DropStudent(int pStudentId, int pCourseId)
        {
            bool success = false;
            var result = _Client.DropStudentFromCourse(pCourseId, pStudentId);

            if (result == null)
            {
                success = true;
            }
            else
            {
            }
            return success;
        }

        /// <summary>
        /// Validates the Auto drop functionality
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            bool success = true;
            var response = _Client.ValidateAutoDrop();

            if (response != null)
            {
                success = false;
            }
            return success;
        }

        #endregion IUniviersityStudentService Implementation

        #region Private Methods

        /// <summary>
        /// This will attempt to rebuild the student details from the failed data thate came through on the the student details API call.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private StudentOverview RebuildStudentDetailsFromCourseRegistry(StudentDetails s)
        {
            UniversityClient client = new UniversityClient();
            s.Courses = new List<EnrolledCourse>();
            var courses = client.GetAllCourses();
            foreach (CourseRoot c in courses)
            {
                var course = client.GetCourse(c.Id);
                var student = course.EnrolledStudents.Find(x => x.Id == s.Id);
                if (student != null)
                {
                    s.Courses.Add(new EnrolledCourse(course, student.Status, student.Grade));
                }
            }

            return new StudentOverview(s);
        }

        #endregion Private Methods
    }
}