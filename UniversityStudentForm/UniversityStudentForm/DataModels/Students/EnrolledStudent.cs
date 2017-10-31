using Newtonsoft.Json;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// This is to hold the details of the student for a course.
    /// </summary>
    public class EnrolledStudent : StudentRoot
    {
        public const string ENROLLED = "Enrolled";
        public const string DROPPED = "Dropped";

        [JsonProperty("grade")]
        public double Grade { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}