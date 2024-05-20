using Newtonsoft.Json;

namespace EvoHub.Domain
{
    public class RepositoryModel
    {
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("incomplete_results")]
        public bool IncompleteResults { get; set; }

        [JsonProperty("items")]
        public GitHubRepository[] Repositories { get; set; }


    }
}
