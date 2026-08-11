using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace JobBoardPlatform.Mvc.Filters
{
    public class HangfireFilter:IDashboardAuthorizationFilter
    {
       
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            return httpContext.User.IsInRole("Admin");
        }
    }
}
