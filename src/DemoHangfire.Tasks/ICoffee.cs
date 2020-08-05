using System;

namespace DemoHangfire.Tasks
{
    public interface ICoffee
    {
        string DisplayName { get; }
        void Prepare(string clientName, string preference);
    }
}
