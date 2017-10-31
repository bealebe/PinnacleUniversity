using PinnacleUniversity.DataModels;
using System.Collections.Generic;

namespace PinnacleUniversity.REST
{
    /// <summary>
    /// Interface for holding the prototypes of the Rest Methods
    /// </summary>
    internal interface IUniversityClient
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