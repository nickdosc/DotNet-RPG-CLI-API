
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
                                                { "info", RegisterUser }
                                            };

        private static userCreationAndInfo userInfo = new userCreationAndInfo();

        private async Task GetUsersAsync()
        {
            List<string> users = await userInfo.GetUsers();
            string formattedUsers = string.Join(" #### ", users);
            string boxedUsers = "### " + formattedUsers + " ###";
            commandResponse = boxedUsers;
        }

        private async Task CreateUser(string input)
        {
            // Split userInfo into username, password, and email
            string[] userFields = input.Split('|');
            if (userFields.Length != 3)
            {
                Console.WriteLine("Invalid registration format. Expected: username|password|email");
                return;
            }

            string username = userFields[0];
            string password = userFields[1];
            string email = userFields[2];

            Console.WriteLine($"Registering user: {username}, Email: {email}");
            await Task.Run(() =>
            {
                // call the actual API or DB logic to register the user
                commandResponse = username;
            });
        }

        private static void RegisterUser(string input)
        {
            // Code to handle "register" command
        }
    }
}
