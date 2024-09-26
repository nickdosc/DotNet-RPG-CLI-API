using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        // Connect to the server running locally on 127.0.0.1:12345
        TcpClient client = new TcpClient("127.0.0.1", 12345);
        NetworkStream stream = client.GetStream();

        Console.WriteLine("Connected to server");
        Console.WriteLine("                           ___\r\n                          ( ((\r\n                           ) ))\r\n  .::.                    / /(\r\n 'M .-;-.-.-.-.-.-.-.-.-/| ((::::::::::::::::::::::::::::::::::::::::::::::.._\r\n(J ( ( ( ( ( ( ( ( ( ( ( |  ))   -====================================-      _.>\r\n `P `-;-`-`-`-`-`-`-`-`-\\| ((::::::::::::::::::::::::::::::::::::::::::::::''\r\n  `::'                    \\ \\(\r\n                           ) ))\r\n                          (_((");
        Console.WriteLine("\r\n█░█░█ █▀▀ █░░ █▀▀ █▀█ █▀▄▀█ █▀▀\r\n▀▄▀▄▀ ██▄ █▄▄ █▄▄ █▄█ █░▀░█ ██▄");
        while (true)
        {
            // Get command input from user
            Console.Write("Enter command: ");
            string message = Console.ReadLine();

            // Exit if the user types "exit"
            if (message.ToLower() == "exit") break;

            // If the user types 'register', ask for the required fields
            if (message.ToLower() == "register")
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
                string command = $"register|{userData}";
                byte[] data = Encoding.ASCII.GetBytes(command);
                stream.Write(data, 0, data.Length);
            }
            else
            {
                // Send any other commands normally
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }

            // Receive response from the server
            byte[] buffer = new byte[1024];
            int byteCount = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, byteCount);

            Console.WriteLine($"Server: {response}");
        }

        // Close the connection
        client.Close();
        Console.WriteLine("Disconnected from server");
    }
}
