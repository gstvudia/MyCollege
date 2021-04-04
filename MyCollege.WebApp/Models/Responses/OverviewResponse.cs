using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyCollege.WebApp.Models.Responses
{
    public class OverviewResponse<TEntity> 
        where TEntity : class
    {
        [JsonProperty(PropertyName = "overviews")]
        public List<TEntity> Overview { get; set; }

        [JsonProperty(PropertyName = "isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public List<string> Errors { get; set; } = new List<string>();

    }
}