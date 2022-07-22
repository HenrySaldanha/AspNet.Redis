namespace Domain
{
    public class GitHubRepository
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public uint Stars { get; set; }
        public DateTime CreateTime { get; set; }
    }
}