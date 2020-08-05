using Hangfire.Dashboard;

namespace DemoHangfire.Dashboard
{
    public class LaxAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //This is unsafe, make sure this app is not used exposed to the world.
            return true;
        }
    }
}
