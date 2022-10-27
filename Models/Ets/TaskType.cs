namespace jira2ets.Models.Ets
{
    public class TaskType
    {
        public string project_id { get; set; }
        public List<Project> task_types { get; set; }
    }
}