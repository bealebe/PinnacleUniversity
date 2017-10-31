using Newtonsoft.Json;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// This class is used to store the properties common to all objects dealing with Courses.
    /// </summary>
    public class CourseRoot
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("creditHours")]
        public double CreditHours { get; set; }
    }
}