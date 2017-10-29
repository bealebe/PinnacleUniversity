using PinnacleUniversity.DataModels;
using PinnacleUniversity.REST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleUniversity.Services
{
    public class UniversityStudentService : IUniversityStudentService
    {

        #region Constructor and Client
        UniversityClient _Client;
        UniversityContext _DB;

        public UniversityStudentService()
        {
            _Client = new UniversityClient();
            _DB = new UniversityContext();
        }

#endregion

        #region IUniviersityStudentService Implementation

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

        public bool Initialize()
        {
            bool success = false;
            try
            {
                ResetDB();
                var students = _Client.GetAllStudents();

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
            }

            return success;
        }

        public async Task<bool> BeginDroppingStudents(BindingList<StudentOverview> pStudents)
        {
            int TotalCoursesDropped = 0;
            foreach (StudentOverview s in pStudents)
            {
                s.Courses.Sort((c1, c2) => c1.Grade.CompareTo(c2.Grade));
                foreach (EnrolledCourse c in s.Courses)
                {
                    if (s.FullTime && c.Grade < 40f && s.ClassOkayToDrop(c))
                    {
                        Console.WriteLine("Dropping Course " + c.Title);
                        c.Status = EnrolledStudent.DROPPED;
                        s.CalculateCreditAndGPA();
                        await Task.Run(() => DropStudent(s.Id, c.CourseId));
                        TotalCoursesDropped++;
                    }
                }
            }
            Console.WriteLine("Total Courses Dropped: " + TotalCoursesDropped);
            return Validate();
        }

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

        public BindingList<StudentOverview> GetAllStudentsFromDB()
        {
            BindingList<StudentOverview> list = null;
            
            var query = from s in _DB.StudentsOverview
                        orderby s.Id
                        select s;
            list = new BindingList<StudentOverview>(query.ToList());
            
            return list;

        }

        public bool ResetDB()
        {
            _Client.ResetAPIData();
            
            _DB.Database.ExecuteSqlCommand("DELETE FROM StudentOverview");
            _DB.SaveChanges();
           
            return true;
        }

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
#endregion

        #region Private Methods
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
                    s.Courses.Add(new EnrolledCourse(course, student.Status, student.Grade, student.Id));
                }
            }

            return new StudentOverview(s);
        }
#endregion
    }
}
