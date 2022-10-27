using System.Globalization;

namespace jira2ets.Models.JIra.Work
{
    public class JiraItem
    {
        public JiraItem()
        {
        }

        private string _started;
        private string _timeSpent;

        public string TimeSpent
        {
            get => _timeSpent;
            set => _timeSpent = ConvertToEtsTime(value);
        }

        public int BillableSeconds { get; set; }
        public int TempoWorklogId { get; set; }
        public int TimeSpentSeconds { get; set; }
        public JiraIssue Issue { get; set; }
        public string Comment { get; set; }
        public string Worker { get; set; }
        public string Updater { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }

        public string Started
        {
            get => _started;
            set => _started = ConvertToDateTime(value);
        }

        public int OriginTaskId { get; set; }
        public int OriginId { get; set; }

        private string ConvertToEtsTime(string value)
        {
            var result = 0.0;

            foreach (var s in value.Split(' '))
            {
                result += MapToDouble(s);
            }

            return result.ToString(CultureInfo.InvariantCulture);
        }

        private double MapToDouble(string value)
        {
            switch (value.Last())
            {
                case 'd': return double.Parse(value.Remove(value.IndexOf('d'))) * 8;
                case 'h': return double.Parse(value.Remove(value.IndexOf('h')));
                case 'm': return double.Parse(value.Remove(value.IndexOf('m'))) * 1 / 60;
                default: throw new InvalidOperationException();
            }
        }

        private string ConvertToDateTime(string value)
        {
            return Convert.ToDateTime(value).ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
        }


        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Started} {Issue.Key} {Issue.Summary} {TimeSpent}";
        }
    }
}