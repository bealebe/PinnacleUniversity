using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinnacleUniversity.REST;
using PinnacleUniversity.DataModels;

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
    }
}
