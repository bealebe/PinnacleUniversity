using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleUniversity.DataModels
{
    public class CourseDetails : CourseRoot
    { 
        [JsonProperty("enrolledStudents")]
        public List<EnrolledStudent> EnrolledStudents { get; set; }
    }
}
