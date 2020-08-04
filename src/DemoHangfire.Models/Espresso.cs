using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DemoHangfire.Models
{
    public class Espresso : ICoffee
    {
        public string DisplayName => "Espresso";
        
        public void Prepare(string clientName, string preference)
        {
            throw new NotImplementedException();
        }
    }
}
