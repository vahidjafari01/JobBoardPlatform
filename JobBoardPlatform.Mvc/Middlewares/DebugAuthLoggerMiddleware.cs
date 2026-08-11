using Microsoft.AspNetCore.Authentication;

namespace JobBoardPlatform.Mvc.Middlewares
{
    public class DebugAuthLoggerMiddleware : IMiddleware
    {
        private readonly string _logPath = @"C:\Users\11\AppData\Local\Temp\opencode\authlog2.txt";
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public DebugAuthLoggerMiddleware(IAuthenticationSchemeProvider schemeProvider)
        {
            _schemeProvider = schemeProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var defAuth = await _schemeProvider.GetDefaultAuthenticateSchemeAsync();

            var before = string.Join(" | ",
                DateTime.Now.ToString("HH:mm:ss.fff"),
                context.Request.Method,
                context.Request.Path,
                "isAuth=" + context.User.Identity?.IsAuthenticated,
                "DefaultAuthenticateScheme=" + defAuth?.Name);
            try { System.IO.File.AppendAllText(_logPath, before + Environment.NewLine); } catch { }

            await next(context);
        }
    }
}
