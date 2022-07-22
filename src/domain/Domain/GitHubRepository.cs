namespace Domain
{
    public class GitHubRepository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public uint Stars { get; set; }
        public DateTime CreateTime { get; set; }
        public RepositoryOwner Owner { get; set; }
    }
    public class RepositoryOwner
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string LoginName { get; set; }
    }
}