using Newtonsoft.Json;

namespace MyCollege.WebApp.Models
{
    public class SubjectOverview
    {
        [JsonProperty(PropertyName = "subject")]
        public SubjectDTO Subject { get; set; }

        [JsonProperty(PropertyName = "averageGrade")]
        public string AverageGrade { get; set; }

        [JsonProperty(PropertyName = "studentsCount")]
        public int StudentsCount { get; set; }
    }
}