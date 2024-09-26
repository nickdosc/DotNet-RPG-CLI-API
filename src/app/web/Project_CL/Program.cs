using Project_CL.commands;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    static void Main()
    {
        // Create a TCP listener on localhost, port 12345
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
        server.Start();
        Console.WriteLine("Server started on localhost:12345");

        while (true)
        {
            // Accept a client connection
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client connected");

            // Handle the client in a new thread
            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }

    static void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int byteCount;

        while ((byteCount = stream.Read(buffer, 0, buffer.Length)) != 0)
        {
            string request = Encoding.ASCII.GetString(buffer, 0, byteCount);
            Console.WriteLine($"Received from client: {request}");
            string response ="";
            commandDictionary commandDictionary = new commandDictionary();

            if (commandDictionary.commandHandlers.ContainsKey(request))
            {
                commandDictionary.commandHandlers[request](request);
                response = commandDictionary.commandResponse;

            }
            else
            {
                response = "Invalid command";
            }

            byte[] responseData = Encoding.ASCII.GetBytes(response);
            stream.Write(responseData, 0, responseData.Length);
        }

        client.Close();
        Console.WriteLine("Client disconnected");
    }
}
