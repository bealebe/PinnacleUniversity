using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleUniversity.DataModels.Courses
{
    public class EnrolledCourse : CourseRoot
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("grade")]
        public double Grade { get; set; }
    }
}
