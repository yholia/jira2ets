namespace jira2ets.Models.JIra.Search
{
    public class Issue
    {
        public string expand { get; set; } 
        public string id { get; set; } 
        public string self { get; set; } 
        public string key { get; set; } 
        public Fields fields { get; set; } 
    }
}