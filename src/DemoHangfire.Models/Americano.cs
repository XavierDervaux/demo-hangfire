using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHangfire.Models
{
    public class Americano : ICoffee
    {
        public string DisplayName => "Americano";

        public void Prepare(string clientName, string preference)
        {
            throw new NotImplementedException();
        }
    }
}
