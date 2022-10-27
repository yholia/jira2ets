namespace jira2ets.Models.JIra.Work
{
    public class JiraIssue
    {
        public int OriginalEstimateSeconds { get; set; }
        public int EstimatedRemainingSeconds { get; set; }
        public bool InternalIssue { get; set; }
        public string IssueStatus { get; set; }
        public string IssueType { get; set; }
        public string Summary { get; set; }
        public int ProjectId { get; set; }
        public List<object> Components { get; set; }
        public string ParentKey { get; set; }
        public string ProjectKey { get; set; }
        public string IconUrl { get; set; }
        public List<object> Versions { get; set; }
        public string Key { get; set; }
        public int Id { get; set; }
    }
}