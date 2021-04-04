using Newtonsoft.Json;
using System;

namespace MyCollege.WebApp.Models
{
    public class TeacherDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "salary")]
        public decimal Salary { get; set; }

        [JsonProperty(PropertyName = "birthDate")]
        public DateTime Birthdate { get; set; }
    }
}