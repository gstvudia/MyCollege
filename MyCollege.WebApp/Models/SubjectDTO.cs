using Newtonsoft.Json;


namespace MyCollege.WebApp.Models
{
    public class SubjectDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string SubjectName { get; set; }

        [JsonProperty(PropertyName = "teacher")]
        public TeacherDTO Teacher { get; set; }

        [JsonProperty(PropertyName = "selectedTeacher")]
        public int SelectedTeacher { get; set; }
    }
}