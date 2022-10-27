using System.Net.Http;
using System.Net.Http.Headers;
using jira2ets.Models.Ets;
using MatBlazor;

using Microsoft.AspNetCore.Components;
namespace jira2ets.Services
{
    public class EtsService
    {
        private readonly HttpClient _client;

        public EtsService(IHttpClientFactory client)
        {
            _client = client.CreateClient();
        }

        public EmployeeDetails EmployeeDetails { get; private set; }

        public async Task SignIn(string user, string password)
        {
            if (!user.StartsWith("ua\\"))
            {
                user = "ua\\" + user;
            }
            
            var auth = new User {username = user, password = password};

            var response1 = await _client.PostAsJsonAsync("https://ets.akvelon.net/api/auth/sign-in", auth);
            var response = await response1.Content.ReadFromJsonAsync<AuthResponse>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.access_token);
            EmployeeDetails = response.employee_details;
        }

        public async Task<List<Project>> GetProjects()
        {
            return await _client.GetJsonAsync<List<Project>>("https://ets.akvelon.net/api/user/projects/");
        }

        public async Task<List<TaskType>> GetTaskTypes(IEnumerable<Project> project)
        {
            var typesPayload = new GetTaskTypesPayload {project_ids = project.Select(s => s.id).ToList()};

            var response= await _client.PostAsJsonAsync("https://ets.akvelon.net/api/task-types/", typesPayload);
            return await response.Content.ReadFromJsonAsync<List<TaskType>>();
        }

        public async Task CreateTimeUnit(TimeUnit timeUnit)
        {
            await _client.PostAsJsonAsync("https://ets.akvelon.net/api/time-units/", timeUnit);
        }

        private class GetTaskTypesPayload
        {
            public List<String> project_ids { get; set; }
        }
    }
}