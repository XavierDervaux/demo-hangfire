using System;

namespace DemoHangfire.Models
{
    public interface ICoffee
    {
        string DisplayName { get; }
        void Prepare(string clientName, string preference);
    }
}
