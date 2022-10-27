using System.Net.Http.Headers;
using jira2ets.Models.Ets;
using MatBlazor;

namespace jira2ets.Services
{
    public class EtsService
    {
        private readonly HttpClient _client;

        public EtsService(IHttpClientFactory client)
        {
            _client = client.CreateClient();
            _client.BaseAddress = new Uri("https://ets.akvelon.net");
        }

        public EmployeeDetails EmployeeDetails { get; private set; }

        public async Task SignIn(string user, string password)
        {
            if (!user.StartsWith("ua\\"))
            {
                user = "ua\\" + user;
            }

            var auth = new User {username = user, password = password};
            var response = await _client.PostAsJsonAsync("/api/auth/sign-in", auth);
            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authResponse?.access_token);

            EmployeeDetails = authResponse.employee_details;
        }

        public async Task<List<Project>> GetActiveProjects()
        {
            try
            {
                var projects = await _client.GetJsonAsync<List<Project>>("/api/user/projects/");

                return projects.Where(s => s.assignments.Any(a => a.status == "Active"))
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new List<Project>();
        }

        public async Task<List<TaskType>> GetTaskTypes(IEnumerable<Project> project)
        {
            if (!project.Any())
            {
                return new List<TaskType>();
            }
            var typesPayload = new GetTaskTypesPayload {project_ids = project.Select(s => s.id).ToList()};

            var response = await _client.PostAsJsonAsync("/api/task-types/", typesPayload);

            return await response.Content.ReadFromJsonAsync<List<TaskType>>() ?? new List<TaskType>();
        }

        public async Task CreateTimeUnit(TimeUnit timeUnit)
        {
            await _client.PostAsJsonAsync("/api/time-units/", timeUnit);
        }

        private class GetTaskTypesPayload
        {
            public List<String> project_ids { get; set; }
        }
    }
}