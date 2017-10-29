using System.Collections.Generic;
using System.ComponentModel;

namespace PinnacleUniversity.DataModels
{
    public class StudentDetails : StudentRoot
    {
        [Browsable(false)]
        public virtual List<EnrolledCourse> Courses { get; set; }
    }
}
