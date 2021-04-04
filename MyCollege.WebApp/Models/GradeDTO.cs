using Newtonsoft.Json;

namespace MyCollege.WebApp.Models
{
    public class GradeDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "selectedSubject")]
        public int Subject { get; set; }

        [JsonProperty(PropertyName = "selectedStudent")]
        public int Student { get; set; }

        [JsonProperty(PropertyName = "value")]
        public int value { get; set; }
    }
}