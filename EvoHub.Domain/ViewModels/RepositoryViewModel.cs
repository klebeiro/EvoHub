namespace EvoHub.Domain
{
    public class RepositoryViewModel
    {     
        public long TotalCount { get; set; }

        public GitHubRepository[] Repositories { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public Owner Owner { get; set; }

        public string Description { get; set; }
    }
}
