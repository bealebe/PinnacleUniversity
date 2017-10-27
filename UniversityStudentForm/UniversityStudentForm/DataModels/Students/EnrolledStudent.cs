using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinnacleUniversity.DataModels
{
    public class EnrolledStudent : StudentRoot
    {
        [JsonProperty("grade")]
        public double Grade { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
