using PinnacleUniversity.DataModels;
using PinnacleUniversity.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PinnacleUniversity.ViewModels
{
    /// <summary>
    /// This will serve as a psuedo view model for the form to use.
    /// </summary>
    public class UniversityStudentsViewModel : IProgress<string>
    {
        public const string PROGRESS_GETTING_DATA_FROM_API = "Gathering Data...";
        public const string PROGRESS_LOADING_FROM_DATABASE = "Loading From Database...";
        public const string PROGRESS_FINISHED = "Finished!";
        public const string PROGRESS_DROPPING_STUDENTS = "Auto Dropping Students...";

        private UniversityStudentService _Service;

        private bool DropValidation = false;
        private IProgress<string> _Progress;

        /// <summary>
        /// Constructor for the View Model that will kick off a thread to perform Activity 1 on the Assessment
        /// </summary>
        /// <param name="pProgress"></param>
        public UniversityStudentsViewModel(IProgress<string> pProgress)
        {
            _Service = new UniversityStudentService();
            _Progress = pProgress;

            //Run the initialization on a different thread.
            Task.Run(() =>
            {
                Report(PROGRESS_GETTING_DATA_FROM_API);
                _Service.Initialize(this);
                Report(PROGRESS_LOADING_FROM_DATABASE);
                Students = _Service.GetAllStudentsFromDB();
                Report(PROGRESS_FINISHED);
            });
        }

        /// <summary>
        /// This will initiate the Auto drop code for Activity 2 on the Assessment
        /// </summary>
        public void BeginDroppingCourses()
        {
            Report(PROGRESS_DROPPING_STUDENTS);
            Task.Run(() =>
            {
                DropValidation = _Service.BeginDroppingStudents(Students, this);
                Students = _Service.GetAllStudentsFromDB();
                Report(PROGRESS_FINISHED);
            });
        }

        /// <summary>
        /// Gets all the courses from the db for a given student via the service.
        /// </summary>
        /// <param name="pStudentId"></param>
        /// <returns></returns>
        public List<EnrolledCourse> GetCoursesFromStudent(int pStudentId)
        {
            return _Service.GetAllStudentCoursesFromDB(pStudentId);
        }

        /// <summary>
        /// Reports a status message back to the GUI
        /// </summary>
        /// <param name="pReport"></param>
        public void Report(string pReport)
        {
            _Progress.Report(pReport);
        }

        /// <summary>
        /// List of students that binds to the DataGridView on the GUI
        /// </summary>
        public BindingList<StudentOverview> Students { get; set; }
    }
}