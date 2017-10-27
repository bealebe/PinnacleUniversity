using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinnacleUniversity.DataModels.Courses;

namespace PinnacleUniversity.DataModels.Students
{
    public class StudentDetails : StudentRoot
    {
        public List<EnrolledCourse> Courses { get; set; }
    }
}
