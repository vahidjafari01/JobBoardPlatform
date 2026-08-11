using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatform.Presentation.Models;
using System.Security.Authentication;
using System.Text.Json;

namespace JobBoardPlatform.Presentation.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException ex:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                case PermisionException ex:
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                case BadRequestException ex:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                case UnauthorizedAccessException ex:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return context.Response.WriteAsync(GenerateResponseBody("Unauthorized-401", ex.Message));
                case ArgumentNullException ex:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return context.Response.WriteAsync(GenerateResponseBody("ArgumentNull-400", ex.Message));
                case ArgumentOutOfRangeException ex:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return context.Response.WriteAsync(GenerateResponseBody("ArgumentOutofRang-400", ex.Message));
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    return context.Response.WriteAsync(GenerateResponseBody(
                        "InternalServerError_500",
                        "Something went wrong. Please contact your administrator."));
            }
        }

        private static string GenerateResponseBody(string code, string message)
        {
            var response = new BaseResponseDto(message, code);

            return JsonSerializer.Serialize(response, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}
