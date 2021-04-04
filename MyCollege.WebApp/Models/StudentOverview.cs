using Newtonsoft.Json;

namespace MyCollege.WebApp.Models
{
    public class StudentOverview
    {
        [JsonProperty(PropertyName = "student")]
        public StudentDTO Student { get; set; }
    }
}