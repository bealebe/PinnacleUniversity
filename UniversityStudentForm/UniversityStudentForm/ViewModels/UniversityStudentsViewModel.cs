using PinnacleUniversity.DataModels;
using PinnacleUniversity.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleUniversity.ViewModels
{
    public class UniversityStudentsViewModel
    {

        public const string PROGRESS_GETTING_DATA_FROM_API = "Gathering Data...";
        public const string PROGRESS_LOADING_FROM_DATABASE = "Loading From Database...";
        public const string PROGRESS_FINISHED = "Finished!";


        UniversityStudentService _Service;

        bool DropValidation = false;
        IProgress<string> _Progress;

        public UniversityStudentsViewModel(IProgress<string> pProgress)
        {
            _Service = new UniversityStudentService();
            _Progress = pProgress;

            Task.Run(() =>
            {
                Report(PROGRESS_GETTING_DATA_FROM_API);
                _Service.Initialize();
                Report(PROGRESS_LOADING_FROM_DATABASE);
                Students = _Service.GetAllStudentsFromDB();
                Report(PROGRESS_FINISHED);
            });
        }

        public void BeginDroppingCourses()
        {
            Task.Run(async () => 
            {
                DropValidation = await _Service.BeginDroppingStudents(Students);
                Students = _Service.GetAllStudentsFromDB();
                Report(DropValidation.ToString());
            });
        }

        public List<EnrolledCourse> GetCoursesFromStudent(int pStudentId)
        {
            return _Service.GetAllStudentCoursesFromDB(pStudentId);
        }

        public void Report(string pReport)
        {
            _Progress.Report(pReport);
        }

        public BindingList<StudentOverview> Students { get; set; }

    }
}
