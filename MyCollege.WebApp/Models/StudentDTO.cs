using Newtonsoft.Json;
using System;

namespace MyCollege.WebApp.Models
{
    public class StudentDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "birthDate")]
        public DateTime Birthdate { get; set; }

        [JsonProperty(PropertyName = "registration")]
        public int RegistrationNumber { get; set; }

        [JsonProperty(PropertyName = "grades")]
        public GradeDTO[] Grades { get; set; }
    }
}