using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// This class is to hold the course details in the database for a student
    /// </summary>
    [Table("EnrolledCourse")]
    public class EnrolledCourse : CourseRoot
    {
        public EnrolledCourse()
        {
        }

        public EnrolledCourse(CourseDetails pCourse, string pStatus, double pGrade)
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