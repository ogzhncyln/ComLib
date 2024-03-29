﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("(Server -> s , Client -> c)");
            Console.Write("> ");
            string cmd = Console.ReadLine();
            if(cmd == "s")
            {
                Server server = new Server();
            }else if(cmd == "c")
            {
                Client client = new Client();
            }
            else
            {
                Console.WriteLine("Invalid command.");
            }
            Console.ReadKey();
        }
    }

    class Client : ClientBehavior
    {
        public Client()
        {
            Connect();
        }

        protected override void Connected()
        {
            Console.WriteLine("Connected.");
            Send(Encoding.Unicode.GetBytes("Hello Server!"));
        }

        protected override void Disconnected()
        {
            Console.WriteLine("Disconnected.");
        }

        protected override void Read(byte[] data)
        {
            Console.WriteLine($"Server > \"{Encoding.Unicode.GetString(data)}\"");
        }

        protected override void Sent(byte[] data)
        {
            Console.WriteLine($" > \"{Encoding.Unicode.GetString(data)}\"");
        }
    }

    class Server : ServerBehavior
    {
        public Server()
        {
            Start();
        }

        protected override void Started()
        {
            Console.WriteLine("Server started.");
        }

        protected override void Stoped()
        {
            Console.WriteLine("Server stoped.");
        }

        protected override void Joined(Client client)
        {
            Console.WriteLine($"Client-{client.id} joined.");
            client.Send(Encoding.Unicode.GetBytes("Hello Client!"));
        }

        protected override void Disconnected(Client client)
        {
            Console.WriteLine($"Client-{client.id} disconnected.");
        }

        protected override void Sent(byte[] data,Client client)
        {
            Console.WriteLine($"Server >> Client-{client.id} : \"{Encoding.Unicode.GetString(data)}\"");
        }

        protected override void Read(byte[] data,Client client)
        {
            Console.WriteLine($"Client-{client.id} > \"{Encoding.Unicode.GetString(data)}\"");
        }
    }
}
