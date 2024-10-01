using System.Net.Http.Json;
using Project_CL.Data.user;

namespace Project_CL.Services
{
    public class ApiService(HttpClient httpClient)
    {
        //We don't need to add controller to the path, just whatever is before controller in the name of the class
        public async Task<User[]> GetUsers(CancellationToken cancellationToken = default) =>
         await httpClient.GetFromJsonAsync<User[]>("/user", cancellationToken) ?? [];
        
        //Get a single user API - For login
        public async Task<User> GetUser(string username, CancellationToken cancellationToken = default)
        {
            var user = await httpClient.GetFromJsonAsync<User>($"/user/{username}", cancellationToken);
            return user ?? throw new InvalidOperationException("User not found");
        }


        public async Task<HttpResponseMessage> CreateUser(User user, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync("/user", user, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");
            }
            response.EnsureSuccessStatusCode();
            return response;
        }

    }
}
