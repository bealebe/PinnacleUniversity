using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinnacleUniversity.DataModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace PinnacleUniversity.REST
{
    /// <summary>
    /// This class will serve as the API web method client.
    /// </summary>
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

        #endregion Constants

        #region Constructor & Client

        private RestClient _Client;

        public UniversityClient()
        {
            _Client = new RestClient(HOST);
        }

        #endregion Constructor & Client

        #region GET Methods

        /// <summary>
        /// Returns all the courses in the University.
        /// </summary>
        /// <returns></returns>
        public List<CourseRoot> GetAllCourses()
        {
            CheckNetwork();
            var request = new RestRequest(COURSE, Method.GET) { RequestFormat = DataFormat.Json };
            var response = _Client.Execute<List<CourseRoot>>(request);
            VerifyGoodResponse(response);

            return response.Data;
        }

        /// <summary>
        /// Returns all the students in the University.
        /// </summary>
        /// <returns></returns>
        public List<StudentRoot> GetAllStudents()
        {
            CheckNetwork();

            var request = new RestRequest(STUDENT, Method.GET) { RequestFormat = DataFormat.Json };
            var response = _Client.Execute<List<StudentRoot>>(request);
            VerifyGoodResponse(response);

            return response.Data;
        }

        /// <summary>
        /// Returns details related to a specific course with a collection of students.
        /// </summary>
        /// <param name="pId">ID for course</param>
        /// <returns></returns>
        public CourseDetails GetCourse(int pId)
        {
            CheckNetwork();

            var request = new RestRequest(string.Format(COURSE_DETAIL, pId), Method.GET) { RequestFormat = DataFormat.Json };
            var response = _Client.Execute<CourseDetails>(request);
            VerifyGoodResponse(response);

            return response.Data;
        }

        /// <summary>
        /// Returns details related to a specific student with a collection of courses.
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public StudentDetails GetStudent(int pId)
        {
            CheckNetwork();

            var request = new RestRequest(string.Format(STUDENT_DETAIL, pId), Method.GET) { RequestFormat = DataFormat.Json };
            var response = _Client.Execute<StudentDetails>(request);
            VerifyGoodResponse(response);

            return response.Data;
        }

        /// <summary>
        /// Returns 200 if Auto-Drop activity results in successful outcome, 400 otherwise.
        /// </summary>
        /// <returns></returns>
        public ErrorResponse ValidateAutoDrop()
        {
            CheckNetwork();

            var request = new RestRequest(VALIDATE, Method.GET) { RequestFormat = DataFormat.Json };
            var response = _Client.Execute<ErrorResponse>(request);
            VerifyGoodResponse(response);

            return response.Data;
        }

        #endregion GET Methods

        #region POST Methods

        /// <summary>
        /// Changes the specified students' status in the specified course from 'Enrolled' to 'Dropped'.
        /// </summary>
        /// <param name="pCourseId">Course to drop.</param>
        /// <param name="pStudentId">Student to drop the course.</param>
        /// <returns></returns>
        public ErrorResponse DropStudentFromCourse(int pCourseId, int pStudentId)
        {
            CheckNetwork();

            var request = new RestRequest(string.Format(DROP_COURSE, pCourseId, pStudentId), Method.POST) { RequestFormat = DataFormat.Json };
            var response = _Client.Execute<ErrorResponse>(request);
            VerifyGoodResponse(response);

            return response.Data;
        }

        /// <summary>
        /// Re-initializes the API test database to clear application state imposed by previous POST methods.
        /// </summary>
        /// <returns></returns>
        public ErrorResponse ResetAPIData()
        {
            CheckNetwork();

            var request = new RestRequest(RESET, Method.POST) { RequestFormat = DataFormat.Json };
            var response = _Client.Execute<ErrorResponse>(request);
            VerifyGoodResponse(response);

            return response.Data;
        }

        #endregion POST Methods

        #region Helper Methods

        /// <summary>
        /// Checks to see if the response was correct, and if not throw an exception.
        /// </summary>
        /// <param name="response"></param>
        private void VerifyGoodResponse(IRestResponse response)
        {
            if (response != null && response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.Content == null)
                {
                    throw new IOException("An Unknown Error Occurred.");
                }
                else
                {
                    dynamic e = JObject.Parse(response.Content);
                    throw new IOException(e.message.Value as string);
                }
            }
        }

        /// <summary>
        /// Checks to see if there is an active internet connection
        /// </summary>
        private void CheckNetwork()
        {
            if (!NetworkMonitor.CanPingOutsideNetwork())
            {
                throw new Exception("No Active Internet Connection");
            }
        }

        #endregion Helper Methods
    }
}