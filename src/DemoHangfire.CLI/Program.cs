using System;

namespace DemoHangfire.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var active = true;

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
            Console.WriteLine("Available commands : help, exit");
        }

        private static bool ExecuteCommand(string v)
        {
            var res = true;

            switch (v)
            {
                case "help": PrintHelp(); break;
                case "exit": res = false; break;
                default: 
                    Console.WriteLine($"Unrecognized command {v}, please use one of the following commands :");
                    PrintHelp();
                    break;
            }

            return res;
        }
    }
}
