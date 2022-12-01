using System.Net;
using System.Net.Http.Headers;
using jira2ets.Clients;
using jira2ets.Models.Ets;
using MatBlazor;

namespace jira2ets.Services
{
    public class EtsService
    {
        private readonly EtsClient _etsClient;

        public EtsService(EtsClient etsClient)
        {
            _etsClient = etsClient;
        }

        public EmployeeDetails EmployeeDetails { get; private set; }

        public async Task SignIn(string user, string password)
        {
            if (!user.StartsWith("ua\\"))
            {
                user = "ua\\" + user;
            }

            var authResponse = await _etsClient.SignIn(user, password);

            EmployeeDetails = authResponse.employee_details;
        }

        public async Task<List<Project>> GetActiveProjects()
        {
            var projects = await _etsClient.GetProjects();

            return projects.Where(s => s.assignments.Any(a => a.status == "Active"))
                .ToList();
        }

        public async Task<List<TaskType>> GetTaskTypes(IEnumerable<Project> project)
        {
            return await _etsClient.GetTaskTypes(project);
        }

        public async Task<HttpResponseMessage> CreateTimeUnit(TimeUnit? timeUnit)
        {
            Task.Delay(3000).Wait();
            return await _etsClient.CreateTimeUnit(timeUnit);
        }
    }
}