namespace jira2ets.Models.Ets
{
    public class EtsItem
    {
        public TimeUnit TimeUnit { get; set; }

        public bool ShouldBeReported { get; set; }

        public bool IsReported { get; set; }

        public bool IsReportFailed { get; set; }
    }
}