using Newtonsoft.Json;

namespace MyCollege.WebApp.Models
{
    public class CourseDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string CourseName { get; set; }

        [JsonProperty(PropertyName = "subjects")]
        public SubjectDTO[] Subjects { get; set; }

        [JsonProperty(PropertyName = "selectedSubjects")]
        public int[] SelectedSubjects { get; set; }
    }
}