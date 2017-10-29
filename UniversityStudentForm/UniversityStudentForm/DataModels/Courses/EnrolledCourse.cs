using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleUniversity.DataModels
{
    [Table("EnrolledCourse")]
    public class EnrolledCourse : CourseRoot
    {
        public EnrolledCourse() { }
        public EnrolledCourse(CourseDetails pCourse, string pStatus, double pGrade, int pStudentId)
        {
            Description = pCourse.Description;
            CreditHours = pCourse.CreditHours;
            Id = pCourse.Id;
            Title = pCourse.Title;
            Status = pStatus;
            Grade = pGrade;
        }

        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("grade")]
        public double Grade { get; set; }
        public virtual List<StudentOverview> Students { get; set; }
        public int CourseId { get; set; }
    }
}
