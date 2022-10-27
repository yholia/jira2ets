using System.Diagnostics.CodeAnalysis;

namespace jira2ets.Models.JIra.Work
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RequestBody
    {
        public string from { get; set; }
        public string to { get; set; }
        public List<string> worker { get; set; }
    }
}