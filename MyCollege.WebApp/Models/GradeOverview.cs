using Newtonsoft.Json;

namespace MyCollege.WebApp.Models
{
    public class GradeOverview
    {
        [JsonProperty(PropertyName = "grade")]
        public GradeDTO Grade { get; set; }
    }
}