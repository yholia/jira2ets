namespace jira2ets.Models.JIra.Search
{
    public class SearchRequest
    {
        public string jql { get; set; } 
        public List<string> fields { get; set; }
    }
}