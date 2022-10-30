using System.Net;
using System.Net.Http.Headers;
using jira2ets.Models.Ets;
using MatBlazor;

namespace jira2ets.Clients;

public class EtsClient
{
    private readonly HttpClient _client;

    public EtsClient(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient();
        _client.BaseAddress = new Uri("https://ets.akvelon.com");
    }

    public async Task<AuthResponse?> SignIn(string user, string password)
    {
        var auth = new User {username = user, password = password};
        var response = await _client.PostAsJsonAsync("/api/auth/sign-in", auth);
        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authResponse?.access_token);

        return authResponse;
    }

    public async Task<List<Project>> GetProjects()
    {
       return await _client.GetJsonAsync<List<Project>>("/api/user/projects/");
    }

    public async Task<List<TaskType>> GetTaskTypes(IEnumerable<Project> project)
    {
        var typesPayload = new GetTaskTypesPayload {project_ids = project.Select(s => s.id).ToList()};

        var response = await _client.PostAsJsonAsync("/api/task-types/", typesPayload);

        return await response.Content.ReadFromJsonAsync<List<TaskType>>();
    }

    public async Task<HttpResponseMessage> CreateTimeUnit(TimeUnit timeUnit)
    {
        return await _client.PostAsJsonAsync("/api/time-units/", timeUnit);
    }

    private class GetTaskTypesPayload
    {
        public List<String> project_ids { get; set; }
    }
}