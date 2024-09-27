using Project_CL.Services;
using Project_CL.Data.user;
using Project_CL.Services.Encryption;

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


        //Get all users
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

        //Create a user
        public async Task<string> CreateUser(string username, string password, string email)
        {
            if (ValidateUser(username, password, email) == "Ok")
            {
                password = EncryptionService.HashPassword(password);
                User user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    Role = "Player"
                };
                await ApiService.CreateUser(user);
                return "User created.";
            }
            return ValidateUser(username, password, email);
        }


        //Validate user for Registration
        public string ValidateUser(string username, string password, string email)
        {
            if (username.Length < 3 || username.Length > 20)
            {
                return "Username must be between 3 and 20 characters.";
            }
            if (password.Length < 6 || password.Length > 20)
            {
                return "Password must be between 6 and 20 characters.";
            }
            if (!email.Contains("@"))
            {
                return "Invalid email address.";
            }
            return "Ok";
        }
    }
}
