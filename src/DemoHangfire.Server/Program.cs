using System;
using DemoHangfire.Helpers;
using Hangfire;

namespace DemoHangfire.Server
{
    class Program
    {
        static void Main()
        {
            var cs = Environment.GetEnvironmentVariable("ConnectionString"); //Fetch the connection string from the environment variable defined in docker-compose.yml. If you do not want to use docker, edit this to be relevant to you.
            
            SqlServerHelper.WaitForSqlServer(cs); //Make sure SQL SErver is started before going further.
            
            GlobalConfiguration.Configuration.UseSqlServerStorage(cs);

            using var server = new BackgroundJobServer();

            var keepAlive = true;
            while (keepAlive)
            {
                Console.WriteLine("Hangfire Server started. Type 'stop' to exit...");
                var read = Console.ReadLine();
                keepAlive = read != "stop";
            }

            server.Dispose();
        }
    }
}