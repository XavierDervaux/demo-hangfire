using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using DemoHangfire.Models;
using Hangfire;
using Hangfire.SqlServer;

namespace DemoHangfire.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for SQL Server...");
            Thread.Sleep(15000); //Wait for SQL Server to be up.

            var active = true;

            var cs = Environment.GetEnvironmentVariable("ConnectionString"); //Fetch the connection string from the environment variable defined in docker-compose.yml. If you do not want to use docker, edit this to be relevant to you.
            GlobalConfiguration.Configuration.UseSqlServerStorage(cs); //Set the configuration for Hangfire so it will use our Instance of SQL Server to store the jobs.
            Directory.CreateDirectory("./output"); //This folder will contain the result of the jobs we'll create.

            PrintWelcome();
            PrintHelp();
            while (active)
            {
                Console.WriteLine("\r\nEnter command:");
                active = ExecuteCommand(Console.ReadLine());
            }
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("--                                             --");
            Console.WriteLine("--                Hangfire demo                --");
            Console.WriteLine("--                                             --");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Available commands : espresso, americano, cappuccino, moka, turkish, help, exit");
        }

        private static bool ExecuteCommand(string v)
        {
            var res = true;

            switch (v)
            {
                case "espresso":
                case "americano":
                case "cappuccino":
                case "moka":
                case "turkish":
                    Console.WriteLine("What's your name ?");
                    var name = Console.ReadLine();
                    Console.WriteLine("Any preference ?");
                    var preference = Console.ReadLine();
                    QueueOrder(v, name, preference);
                    break;
                case "help": PrintHelp(); break;
                case "exit": res = false; break;
                default: 
                    Console.WriteLine($"Unrecognized command {v}, please use one of the following commands :");
                    PrintHelp();
                    break;
            }

            return res;
        }

        private static void QueueOrder(string type, string name, string preference)
        {
            ICoffee brew;

            switch (type)
            {
                case "americano": brew = new Americano(); break;
                case "cappuccino": brew = new Cappuccino(); break;
                case "moka": brew = new Moka(); break;
                case "turkish": brew = new Turkish(); break;
                case "espresso":
                default: brew = new Espresso(); break;
            }

            BackgroundJob.Enqueue(() => brew.Prepare(name, preference));
            Console.WriteLine("Order queued, check output folder later for result.");
        }
    }
}
