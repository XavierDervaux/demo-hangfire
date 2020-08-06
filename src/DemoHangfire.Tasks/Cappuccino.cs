using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DemoHangfire.Tasks
{
    public class Cappuccino
    {
        public string DisplayName => "Cappuccino";

        public async Task Prepare(string clientName, string preference)
        {
            var waterAwaiter = BoilWater();
            var cPowderAwaiter = GrindCoffee();
            var milkAwaiter = FoamMilk();

            PrepareCup();
            PourSugar();

            await waterAwaiter;
            await cPowderAwaiter;
            var coffeeAwaiter = MakeCoffee();

            await milkAwaiter;
            PourFoamedMilk();

            await coffeeAwaiter;
            PourCoffee();
            Serve(clientName, preference);
        }

        /// <summary>
        /// Creates a file to signify the end of the task.
        /// Please note that this file will be on the server, and not on the client.
        /// This also highlights that in the scenario of multiple server instances running, the files will be created only on the server taking the job.
        /// </summary>
        private void Serve(string clientName, string preference)
        {
            Directory.CreateDirectory("Output"); //Please note this is on the current server and NOT the client.
            var filename = $"Output/cappuccino_{clientName}_{Guid.NewGuid()}";
            File.WriteAllText(filename, preference + "\n");
        }

        //
        // Nothing too interesting below this point.
        // These methods exist just to create delay so the job doesn't end too quickly.
        // They stall for a total of 30 seconds (Not including processing time)
        //

        private async Task BoilWater()
        {
            await Task.Delay(TimeSpan.FromSeconds(6));
        }
        private async Task GrindCoffee()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
        private async Task FoamMilk()
        {
            await Task.Delay(TimeSpan.FromSeconds(4));
        }
        private void PrepareCup()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
        private void PourSugar()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
        private async Task MakeCoffee()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
        private void PourFoamedMilk()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
        private void PourCoffee()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}
