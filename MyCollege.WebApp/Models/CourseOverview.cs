using Newtonsoft.Json;

namespace MyCollege.WebApp.Models
{
    public class CourseOverview
    {
        [JsonProperty(PropertyName = "course")]
        public CourseDTO Course { get; set; }

        [JsonProperty(PropertyName = "averageGrade")]
        public string AverageGrade { get; set; }

        [JsonProperty(PropertyName = "studentsCount")]
        public int StudentsCount { get; set; }

        [JsonProperty(PropertyName = "teachersCount")]
        public int TeachersCount { get; set; }
    }
}