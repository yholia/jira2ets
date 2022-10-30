using System.Net.Http.Headers;
using System.Text;
using jira2ets.Clients;
using jira2ets.Models.JIra;
using jira2ets.Models.JIra.Search;
using jira2ets.Models.JIra.Work;
using MatBlazor;

namespace jira2ets.Services
{
    public class JiraService
    {
        private readonly JiraClient _jiraClient;
        private string? _userKey;

        public JiraService(JiraClient jiraJiraClient)
        {
            _jiraClient = jiraJiraClient;
        }

        public async Task SignIn(string user, string password)
        {
            var jiraUser = await _jiraClient.SignIn(user, password);
            _userKey = jiraUser.key;
        }

        public async Task<List<JiraItem>> GetWorklogs(DateTime? from, DateTime? to)
        {
            return await _jiraClient.GetWorklogs(from, to, _userKey);
        }

        public async Task<IDictionary<string, Issue>> GetIssuesAsync(IEnumerable<string>? keys)
        {
            var keyList = keys?.ToList();
            if (keyList == null || !keyList.Any())
            {
                return new Dictionary<string, Issue>();
            }

            return await _jiraClient.GetIssuesAsync(keyList);
        }
    }
}