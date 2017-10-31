using PinnacleUniversity.DataModels;
using System;
using System.ComponentModel;

namespace PinnacleUniversity.Services
{
    /// <summary>
    /// This interface is the container for the prototypes to interact with both the DB and the API Client
    /// </summary>
    public interface IUniversityStudentService
    {
        bool Initialize(IProgress<string> progress);

        bool ResetDB();

        bool DropStudent(int pStudentId, int pCourseId);

        bool Validate();

        BindingList<StudentOverview> GetAllStudentsFromDB();

        bool BeginDroppingStudents(BindingList<StudentOverview> pStudents, IProgress<string> pProgress);
    }
}