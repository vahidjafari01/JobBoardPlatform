using JobBoardPlatfomr.Services.BussinesExceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace JobBoardPlatform.Mvc.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, message) = exception switch
            {
                NotFoundException ex => (StatusCodes.Status404NotFound, ex.Message),
                PermisionException ex => (StatusCodes.Status403Forbidden, ex.Message),
                BadRequestException ex => (StatusCodes.Status400BadRequest, ex.Message),
                UnauthorizedAccessException ex => (StatusCodes.Status401Unauthorized, ex.Message),
                _ => (StatusCodes.Status500InternalServerError, "Something went wrong. Please try again.")
            };

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.Redirect($"/Home/Error?code={statusCode}&message={Uri.EscapeDataString(message)}");
            return true;
        }
    }
}
