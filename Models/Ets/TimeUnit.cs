namespace jira2ets.Models.Ets
{
    public class TimeUnit
    {
        public string project_id { get; set; }
        public string task_type_id { get; set; }
        public string project_title { get; set; }
        public string task_type_title { get; set; }
        public int minutes { get; set; }
        public bool overtime { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public bool isEven { get; set; }
        public bool is_billable { get; set; }
        public bool is_overtime_payable { get; set; }
        public int overtime_multiplier { get; set; }
        public string time { get; set; }
        public string employee_id { get; set; }
        public object next_time_unit_id { get; set; }
    }
}