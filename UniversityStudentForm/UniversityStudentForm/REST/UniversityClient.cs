using RestSharp;
using System;
using System.Collections.Generic;
using PinnacleUniversity.DataModels;

namespace PinnacleUniversity.REST
{
    public class UniversityClient : IUniversityClient
    {
    #region Constants
        public const string HOST = "http://university.pinnstrat.com:8888/";
        public const string STUDENT = "Student/";
        public const string STUDENT_DETAIL = STUDENT + "{0}";
        public const string COURSE = "Course/";
        public const string COURSE_DETAIL = COURSE + "{0}";
        public const string DROP_COURSE = COURSE_DETAIL + "/Drop/{1}";
        public const string VALIDATE = "Validate";
        public const string RESET = "Reset";
        #endregion

    #region Constructor & Client
        private RestClient _Client;

        public UniversityClient()
        {
            _Client = new RestClient(HOST);
        }
        #endregion

    #region GET Methods

        /// <summary>
        /// Returns all the courses in the University.
        /// </summary>
        /// <returns></returns>
        public List<CourseRoot> GetAllCourses()
        {
            try
            {
                var request = new RestRequest(COURSE, Method.GET) { RequestFormat = DataFormat.Json };
                var response = _Client.Execute<List<CourseRoot>>(request);
                return response.Data;
            }
            catch(Exception e)
            {
                //TODO Handle this.
                return null;
            }
        }

        /// <summary>
        /// Returns all the students in the University.
        /// </summary>
        /// <returns></returns>
        public List<StudentRoot> GetAllStudents()
        {
            try
            {
                var request = new RestRequest(STUDENT, Method.GET) { RequestFormat = DataFormat.Json };
                var response = _Client.Execute<List<StudentRoot>>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                //TODO Handle this.
                return null;
            }
        }

        /// <summary>
        /// Returns details related to a specific course with a collection of students.
        /// </summary>
        /// <param name="pId">ID for course</param>
        /// <returns></returns>
        public CourseDetails GetCourse(int pId)
        {
            try
            {
                var request = new RestRequest(string.Format(COURSE_DETAIL, pId), Method.GET) { RequestFormat = DataFormat.Json };
                var response = _Client.Execute<CourseDetails>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                //TODO Handle this.
                return null;
            }
        }

        /// <summary>
        /// Returns details related to a specific student with a collection of courses.
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public StudentDetails GetStudent(int pId)
        {
            try
            {
                var request = new RestRequest(string.Format(STUDENT_DETAIL, pId), Method.GET) { RequestFormat = DataFormat.Json };
                var response = _Client.Execute<StudentDetails>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                //TODO Handle this.
                return null;
            }
        }

        /// <summary>
        /// Returns 200 if Auto-Drop activity results in successful outcome, 400 otherwise.
        /// </summary>
        /// <returns></returns>
        public ErrorResponse ValidateAutoDrop()
        {
            try
            {
                var request = new RestRequest(VALIDATE, Method.GET) { RequestFormat = DataFormat.Json };
                var response = _Client.Execute<ErrorResponse>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                //TODO Handle this.
                return null;
            }
        }

        #endregion

    #region POST Methods

        /// <summary>
        /// Changes the specified students' status in the specified course from 'Enrolled' to 'Dropped'.
        /// </summary>
        /// <param name="pCourseId">Course to drop.</param>
        /// <param name="pStudentId">Student to drop the course.</param>
        /// <returns></returns>
        public ErrorResponse DropStudentFromCourse(int pCourseId, int pStudentId)
        {
            try
            {
                var request = new RestRequest(string.Format(DROP_COURSE, pCourseId, pStudentId), Method.POST) { RequestFormat = DataFormat.Json };
                var response = _Client.Execute<ErrorResponse>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                //TODO Handle this.
                return new ErrorResponse(e.Message);
            }
        }

        /// <summary>
        /// Re-initializes the API test database to clear application state imposed by previous POST methods.
        /// </summary>
        /// <returns></returns>
        public ErrorResponse ResetAPIData()
        {
            try
            {
                var request = new RestRequest(RESET, Method.POST) { RequestFormat = DataFormat.Json };
                var response = _Client.Execute<ErrorResponse>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                //TODO Handle this.
                return new ErrorResponse();
            }
        }

        #endregion
    }
}
