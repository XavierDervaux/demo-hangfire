using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
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
            var cPowdedAwaiter = GrindCoffee();
            var milkAwaiter = FoamMilk();

            PrepareCup();
            PourSugar();

            await waterAwaiter;
            await cPowdedAwaiter;
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
        /// This also highlight that in the scenario of mulitple server instances running, the files will be created only on the server taking the job.
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
        //

        private void PourCoffee()
        {
            Thread.Sleep(2 * 1000);
        }
        private void PourFoamedMilk()
        {
            Thread.Sleep(2 * 1000);
        }
        private async Task MakeCoffee()
        {
            await Task.Delay(10 * 1000);
        }
        private void PourSugar()
        {
            Thread.Sleep(2*1000);
        }
        private void PrepareCup()
        {
            Thread.Sleep(2 * 1000);
        }
        private async Task FoamMilk()
        {
            await Task.Delay(4 * 1000);
        }
        private async Task GrindCoffee()
        {
            await Task.Delay(3 * 1000);
        }
        private async Task BoilWater()
        {
            await Task.Delay(6 * 1000);
        }
    }
}
