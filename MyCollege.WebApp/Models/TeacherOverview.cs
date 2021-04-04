using Newtonsoft.Json;

namespace MyCollege.WebApp.Models
{
    public class TeacherOverview
    {
        [JsonProperty(PropertyName = "teacher")]
        public TeacherDTO Teacher { get; set; }
    }
}