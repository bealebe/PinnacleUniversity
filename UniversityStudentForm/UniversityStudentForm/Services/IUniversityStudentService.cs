using PinnacleUniversity.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleUniversity.Services
{
    public interface IUniversityStudentService
    {

        bool Initialize();
        bool ResetDB();
        bool DropStudent(int pStudentId, int pCourseId);
        bool Validate();
        BindingList<StudentOverview> GetAllStudentsFromDB();
        Task<bool> BeginDroppingStudents(BindingList<StudentOverview> pStudents);
    }
}
