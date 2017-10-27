using System.Collections.Generic;
using PinnacleUniversity.DataModels;
using PinnacleUniversity.DataModels.Courses;
using PinnacleUniversity.DataModels.Students;

namespace PinnacleUniversity.REST
{
    interface IUniversityClient
    {
        List<StudentRoot> GetAllStudents();
        StudentDetails GetStudent(int pId);
        List<CourseRoot> GetAllCourses();
        CourseDetails GetCourse(int pId);

        ErrorResponse DropStudentFromCourse(int pCourseId, int pStudentId);
        ErrorResponse ValidateAutoDrop();
        ErrorResponse ResetAPIData();
    }
}
