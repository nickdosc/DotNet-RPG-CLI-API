using Azure;
using System.Net.Sockets;
using System.Text;

namespace Project_CL.commands
{
    public class commandDictionary
    {

        public string commandResponse = "";

        public Dictionary<string, Action<string>> commandHandlers => new Dictionary<string, Action<string>>()
                                            {
                                                { "players", input => GetUsersAsync().Wait() },
                                                { "register", input => CreateUser(input).Wait() },
                                                { "login", input => LoginUser(input).Wait() }
                                            };

        private static userCreationAndInfo userInfo = new userCreationAndInfo();

        private async Task GetUsersAsync()
        {
            List<string> users = await userInfo.GetUsers();
            string formattedUsers = string.Join(" #### ", users);
            string boxedUsers = "### " + formattedUsers + " ###";
            commandResponse = boxedUsers;
        }

        public async Task CreateUser(string input)
        {
            // Split userInfo into username, password, and email
            string[] userFields = input.Split('|');
            if (userFields.Length != 4)
            {
                Console.WriteLine("Invalid registration format. Expected: username|password|email");
                commandResponse = "Invalid registration format. Expected: username|password|email";
                return;
            }

            string username = userFields[1];
            string password = userFields[2];
            string email = userFields[3];

            string result = await userInfo.CreateUser(username, password, email);

            Console.WriteLine($"Registering user: {username}, Email: {email}");
            commandResponse = result;
        }

        public async Task LoginUser(string input)
        {
            // Split userInfo into username, password, and email
            string[] userFields = input.Split('|');
            if (userFields.Length != 4)
            {
                Console.WriteLine("Invalid registration format. Expected: username|password|email");
                return;
            }
            string username = userFields[1];
            string password = userFields[2];

            string result = await userInfo.LoginUser(username, password);
            Console.WriteLine($"Logging in user: {username}");
            commandResponse = result;

        }
    }
}
