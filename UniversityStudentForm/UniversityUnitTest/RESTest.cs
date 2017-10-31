using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinnacleUniversity.DataModels;
using PinnacleUniversity.REST;
using PinnacleUniversity.Services;
using System.Collections.Generic;

namespace UniversityUnitTest
{
    [TestClass]
    public class RESTest
    {
        [TestMethod]
        public void TestAllREST()
        {
            UniversityClient client = new UniversityClient();

            var courses = client.GetAllCourses();
            Assert.AreNotEqual(null, courses);

            var students = client.GetAllStudents();
            Assert.AreNotEqual(null, students);

            foreach (CourseRoot c in courses)
            {
                var course = client.GetCourse(c.Id);
                Assert.AreNotEqual(null, course);
            }

            foreach (StudentRoot s in students)
            {
                var student = client.GetCourse(s.Id);
                Assert.AreNotEqual(null, student);
            }

            var reset = client.ResetAPIData();
            Assert.AreEqual(null, reset);

            var validate = client.ValidateAutoDrop();
            Assert.AreNotEqual(null, validate);
        }

        [TestMethod]
        public void StudentOverviewETL()
        {
            UniversityStudentService service = new UniversityStudentService();
            service.Initialize();
        }

        [TestMethod()]
        public void GetAllStudentsFromDBTest()
        {
            UniversityStudentService service = new UniversityStudentService();
            service.Initialize();
            var result = service.GetAllStudentsFromDB();
            Assert.AreNotEqual(null, result);
        }

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
    }
}