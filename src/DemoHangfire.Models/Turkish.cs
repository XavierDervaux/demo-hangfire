using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHangfire.Models
{
    public class Turkish : ICoffee
    {
        public string DisplayName => "Turkish";

        public void Prepare(string clientName, string preference)
        {
            throw new NotImplementedException();
        }
    }
}
