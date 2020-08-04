using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHangfire.Models
{
    public class Cappuccino : ICoffee
    {
        public string DisplayName => "Cappuccino";

        public void Prepare(string clientName, string preference)
        {
            throw new NotImplementedException();
        }
    }
}
