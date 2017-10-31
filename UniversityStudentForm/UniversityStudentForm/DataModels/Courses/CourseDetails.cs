using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// This class is to hold the details from the API Course JSON object
    /// </summary>
    public class CourseDetails : CourseRoot
    {
        [JsonProperty("enrolledStudents")]
        public List<EnrolledStudent> EnrolledStudents { get; set; }
    }
}