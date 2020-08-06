using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoHangfire.Tasks
{
    public class CheeseCake
    {
        public string DisplayName => "CheeseCake";

        public void Prepare(string clientName)
        {
            BuyACheapCakeAtCarrefour();
            SellItTwiceThePrice();
            Serve(clientName);
        }

        /// <summary>
        /// Creates a file to signify the end of the task.
        /// Please note that this file will be on the server, and not on the client.
        /// This also highlight that in the scenario of mulitple server instances running, the files will be created only on the server taking the job.
        /// </summary>
        private void Serve(string clientName)
        {
            Directory.CreateDirectory("Output"); //Please note this is on the current server and NOT the client.
            var filename = $"Output/cheesecake_{clientName}_{Guid.NewGuid()}";
            File.WriteAllText(filename, "100% hand-made.\n");
        }

        //
        // Nothing too interesting below this point.
        // These methods exist just to create delay so the job doesn't end too quickly.
        //

        private void SellItTwiceThePrice()
        {
            Thread.Sleep(2*1000);
        }
        private void BuyACheapCakeAtCarrefour()
        {
            Thread.Sleep(10 * 1000);
        }
    }
}
