using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHangfire.Tasks
{
    public class Moka : ICoffee
    {
        public string DisplayName => "Moka";

        public void Prepare(string clientName, string preference)
        {
            throw new NotImplementedException();
        }
    }
}
