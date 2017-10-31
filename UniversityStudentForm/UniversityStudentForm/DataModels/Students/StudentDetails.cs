using System.Collections.Generic;
using System.ComponentModel;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// This class is to hold the details from the API Course JSON object
    /// </summary>
    public class StudentDetails : StudentRoot
    {
        [Browsable(false)]
        public virtual List<EnrolledCourse> Courses { get; set; }
    }
}