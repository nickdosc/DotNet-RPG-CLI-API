using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using Spectre.Console;

class Client
{
    static void Main()
    {
        // Connect to the server running locally on 127.0.0.1:12345
        TcpClient client = new TcpClient("127.0.0.1", 12345);
        NetworkStream stream = client.GetStream();
        bool loggedIn;
        Console.WriteLine("Connected to server");
        Console.WriteLine("                           ___\r\n                          ( ((\r\n                           ) ))\r\n  .::.                    / /(\r\n 'M .-;-.-.-.-.-.-.-.-.-/| ((::::::::::::::::::::::::::::::::::::::::::::::.._\r\n(J ( ( ( ( ( ( ( ( ( ( ( |  ))   -====================================-      _.>\r\n `P `-;-`-`-`-`-`-`-`-`-\\| ((::::::::::::::::::::::::::::::::::::::::::::::''\r\n  `::'                    \\ \\(\r\n                           ) ))\r\n                          (_((");
        Console.WriteLine("\r\n█░█░█ █▀▀ █░░ █▀▀ █▀█ █▀▄▀█ █▀▀\r\n▀▄▀▄▀ ██▄ █▄▄ █▄▄ █▄█ █░▀░█ ██▄");

        // Define available commands for single-selection
        string[] availableCommands = new[] { "register", "login", "players", "attack", "inventory", "help", "quit" };

        while (true)
        {
            // Single-select a command
            var selectedCommand = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a command to execute (use [green]arrow keys[/] to navigate, [blue]Enter[/] to confirm):")
                    .PageSize(10)
                    .AddChoices(availableCommands));

            if (selectedCommand.ToLower() == "quit")
            {
                // Exit if the user selects "quit"
                client.Close();
                Console.WriteLine("Disconnected from server");
                return;
            }

            // Handle registration process
            if (selectedCommand.ToLower() == "register")
            {
                // Collect user registration details
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();

                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                Console.Write("Enter Email: ");
                string email = Console.ReadLine();

                // Format the user registration details into a single string
                string userData = $"{username}|{password}|{email}";

                // Send the "register" command with user data to the server
                string registerCommand = $"register|{userData}";
                byte[] data = Encoding.ASCII.GetBytes(registerCommand);
                stream.Write(data, 0, data.Length);
            }
            else if (selectedCommand.ToLower() == "login")
            {
                // Collect user login details
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();

                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                // Format the user login details into a single string
                string userData = $"{username}|{password}";

                // Send the "login" command with user data to the server
                string loginCommand = $"login|{userData}";
                byte[] data = Encoding.ASCII.GetBytes(loginCommand);
                stream.Write(data, 0, data.Length);
            }
            else
            {
                // Send other commands normally
                byte[] data = Encoding.ASCII.GetBytes(selectedCommand);
                stream.Write(data, 0, data.Length);
            }

            // Receive response from the server
            byte[] buffer = new byte[1024];
            int byteCount = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, byteCount);
            if (response.Equals("Login successful"))
            {
                loggedIn = true;
            }

            Console.WriteLine($"Server: {response}");
        }
    }
}
