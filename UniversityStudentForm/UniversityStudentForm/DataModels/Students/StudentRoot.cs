using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// This class is used to store the properties common to all objects dealing with Students.
    /// </summary>
    public class StudentRoot
    {
        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}