using Project_CL.Services;

namespace Project_CL.commands
{
    public class userCreationAndInfo
    {
        HttpClient client = new()
        {
            BaseAddress = new Uri("http://localhost:5111")
        };
        ApiService ApiService;

        public userCreationAndInfo()
        {
            ApiService = new ApiService(client);
        }

        public async Task<List<string>> GetUsers()
        {
            var users = await ApiService.GetUsers();
            List<string> response = [];
            if (users.Length == 0)
            {
                
                Console.WriteLine("No users found.");
                return response;
            }
            foreach (var user in users)
            {
                response.Add(user.Username);
                Console.WriteLine(user.Username);
            }
            return response;
        }
    }
}
