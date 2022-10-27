using System.Net.Http.Headers;
using System.Text;
using jira2ets.Models.JIra;
using jira2ets.Models.JIra.Search;
using jira2ets.Models.JIra.Work;
using MatBlazor;

namespace jira2ets.Services
{
    public class JiraService
    {
        private readonly HttpClient _client;
        private string _userKey;

        public JiraService(IHttpClientFactory client)
        {
            _client = client.CreateClient();
        }

        public async Task SignIn(string user, string password)
        {
            var token = ToBase64String(user, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            
            var jiraUser = await GetSelf(user);
            _userKey = jiraUser.key;
        }

        public async Task<List<JiraItem>> GetWorklogs(DateTime? from, DateTime? to)
        {
            const string format = "yyyy-MM-dd";
            var requestBody = new RequestBody
            {
                from = from?.ToLocalTime().ToString(format),
                to = to?.ToLocalTime().ToString(format),
                worker = new List<string> {_userKey}
            };

            
            var response = await _client.PostAsJsonAsync("https://jira.tideworks.com/rest/tempo-timesheets/4/worklogs/search", requestBody);
            return await response?.Content?.ReadFromJsonAsync<List<JiraItem>>();
        }

        public async Task<IDictionary<string, Issue>> GetIssuesAsync(IEnumerable<string> keys)
        {
            var keyList = keys?.ToList();
            if (keyList == null || !keyList.Any())
            {
                return new Dictionary<string, Issue>();
            }
            
            var result = await _client.PostAsJsonAsync("https://jira.tideworks.com/rest/api/2/search", new SearchRequest()
            {
                jql = $"issue in ({string.Join(", ", keyList.ToArray())})",
                fields = new List<string>() {"key", "summary"}
            });

            var result2 = await result.Content.ReadFromJsonAsync<SearchResult>();
                
                return result2.issues.ToDictionary(s => s.key, s => s);
        }

        private async Task<User> GetSelf(String username)
        {
            return await _client.GetJsonAsync<User>($"https://jira.tideworks.com/rest/api/2/user?username={username}");
        }

        private static string ToBase64String(string user, string password)
        {
            return Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + password));
        }
    }
}