namespace jira2ets.Models.Ets
{
    public class AuthResponse
    {
        public string access_token { get; set; } 
        public string refresh_token { get; set; } 
        public EmployeeDetails employee_details { get; set; } 
    }
}