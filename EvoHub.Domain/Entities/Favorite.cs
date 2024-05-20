namespace EvoHub.Domain
{
    public class Favorite
    {
        public long Id { get; set; }

        public string? Description { get; set; }

        public string Language { get; set; }

        public DateTimeOffset UpdateLast { get; set; }

        public string Owner { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
