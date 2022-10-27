namespace jira2ets.Models.Ets
{
    public class Project
    {
        public string id { get; set; }
        public string title { get; set; }
        public List<Assignment> assignments { get; set; }
    }
}