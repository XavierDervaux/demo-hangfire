using System;
using DemoHangfire.Helpers;
using DemoHangfire.Tasks;
using Hangfire;

namespace DemoHangfire.CLI
{
    class Program
    {
        static void Main()
        {
            var active = true;

            var cs = Environment.GetEnvironmentVariable("ConnectionString"); //Fetch the connection string from the environment variable defined in docker-compose.yml. If you do not want to use docker, edit this to be relevant to you.
            SqlServerHelper.WaitForSqlServer(cs); //Make sure SQL SErver is started before going further.
            GlobalConfiguration.Configuration.UseSqlServerStorage(cs); //Set the configuration for Hangfire so it will use our Instance of SQL Server to store the jobs.

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
            Console.WriteLine("Available commands : justCoffee, morningCoffee, takeOutCoffee, coffeeAndCake, help, exit");
        }
        private static (string name, string pref) AskNameAndPref()
        {
            Console.WriteLine("What's your name ?");
            var name = Console.ReadLine();

            Console.WriteLine("Any preference ?");
            var preference = Console.ReadLine();

            return (name, preference);
        }
        private static bool ExecuteCommand(string v)
        {
            var res = true;

            switch (v)
            {
                case "justCoffee": JustCoffee(); break; //Just a coffee
                case "morningCoffee": MorningCoffee(); break; //Recuring coffee to be made every morning
                case "takeOutCoffee": TakeOutCoffee(); break; //A coffee I'll come and fetch in 20 minutes
                case "coffeeAndCake": CoffeeAndCake(); break; //A coffee, and a fresh cake

                case "help": PrintHelp(); break;
                case "exit": res = false; break;
                default:
                    Console.WriteLine($"Unrecognized command {v}, please use one of the following commands :");
                    PrintHelp();
                    break;
            }

            return res;
        }




        /*
         *
         * Here's the interesting part :
         *
        */


        /// <summary>
        /// This demonstrates Hangfire's Fire-and-Forget jobs.
        /// By enqueuing a job, it will be executed just once by the server once it is available.
        /// For more information on how jobs are stored and how to preserve context, visit the following link :
        /// https://docs.hangfire.io/en/latest/background-methods/index.html
        /// </summary>
        private static void JustCoffee()
        {
            var (name, pref) = AskNameAndPref();
            var coffee = new Cappuccino();

            BackgroundJob.Enqueue(() => coffee.Prepare(name, pref));

            Console.WriteLine("Cappucinno queued!");
        }

        /// <summary>
        /// This demonstrates Hangfire's Recuring jobs.
        /// By add a job, it will be executed recuringly as specified by the cron Schedule. In this example, Daily.
        /// https://docs.hangfire.io/en/latest/background-methods/performing-recurrent-tasks.html
        /// </summary>
        private static void MorningCoffee()
        {
            var (name, pref) = AskNameAndPref();
            var coffee = new Cappuccino();

            RecurringJob.AddOrUpdate(() => coffee.Prepare(name, pref), Cron.Daily);
            
            Console.WriteLine("Morning coffee planned!");
        }

        /// <summary>
        /// This demonstrates Hangfire's Delayed jobs.
        /// By scheduling a job, it will be executed once when specified. In this example, in 5 minutes.
        /// https://docs.hangfire.io/en/latest/background-methods/calling-methods-with-delay.html
        /// </summary>
        private static void TakeOutCoffee()
        {
            var (name, pref) = AskNameAndPref();
            var coffee = new Cappuccino();

            BackgroundJob.Schedule(() => coffee.Prepare(name, pref), TimeSpan.FromMinutes(5));

            Console.WriteLine("The coffee will be made in 5 minutes!");
        }

        /// <summary>
        /// This demonstrates Hangfire's job Continuations.
        /// Every job generates an ID, by using this id with a continuation we can execute another job once the previous one has finished.
        /// In this example, 'coffee.Prepare(name, pref)' will only be executed after 'cake.Prepare()' returns.
        /// This uses the same logic as Fire-and-Forget jobs, only chained.
        /// </summary>
        private static void CoffeeAndCake()
        {
            var (name, pref) = AskNameAndPref();
            var coffee = new Cappuccino();
            var cake = new CheeseCake();

            var cakeJobId = BackgroundJob.Enqueue(() => cake.Prepare(name));
            BackgroundJob.ContinueJobWith(cakeJobId, () => coffee.Prepare(name, pref));

            Console.WriteLine("Coffee and cake planned!");
        }
    }
}
