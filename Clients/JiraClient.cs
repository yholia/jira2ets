using System.Net.Http.Headers;
using System.Text;
using jira2ets.Models.JIra;
using jira2ets.Models.JIra.Search;
using jira2ets.Models.JIra.Work;
using MatBlazor;

namespace jira2ets.Clients;

public class JiraClient
{
    private readonly HttpClient _client;

    public JiraClient(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient();
        _client.BaseAddress = new Uri("https://jira.tideworks.com");
    }

    public async Task<User> SignIn(string user, string password)
    {
        var token = ToBase64String(user, password);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

        try
        {
            return await _client.GetJsonAsync<User>($"/rest/api/2/user?username={user}");
        }
        catch (HttpRequestException e)
        {
            throw new Exception("Jira authorization is failed: " + e.StatusCode);
        }
    }

    public async Task<List<JiraItem>> GetWorklogs(DateTime? from, DateTime? to, string userKey)
    {
        const string format = "yyyy-MM-dd";
        var requestBody = new RequestBody
        {
            from = from?.ToLocalTime().ToString(format) ?? throw new InvalidOperationException(),
            to = to?.ToLocalTime().ToString(format) ?? throw new InvalidOperationException(),
            worker = new List<string?> {userKey}
        };


        var responseMessage = await _client.PostAsJsonAsync("/rest/tempo-timesheets/4/worklogs/search", requestBody);
        return await responseMessage.Content.ReadFromJsonAsync<List<JiraItem>>() ??
               throw new InvalidOperationException();
    }

    public async Task<IDictionary<string, Issue>> GetIssuesAsync(IEnumerable<string> keyList)
    {
        var responseMessage = await _client.PostAsJsonAsync("/rest/api/2/search", new SearchRequest()
        {
            jql = $"issue in ({string.Join(", ", keyList.ToArray())})",
            fields = new List<string>() {"key", "summary"}
        });

        var searchResult = await responseMessage.Content.ReadFromJsonAsync<SearchResult>();

        return searchResult?.issues.ToDictionary(s => s.key, s => s) ?? throw new InvalidOperationException();
    }

    private static string ToBase64String(string user, string password)
    {
        return Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + password));
    }
}