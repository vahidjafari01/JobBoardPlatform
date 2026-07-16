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
                HandleExceptionAsync(context, e);
            }
        }

        private void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case NotFoundException ex:
                    context.Response.StatusCode = 404;
                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case PermisionException ex:
                    context.Response.StatusCode = 403;
                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case BadRequestException ex:
                    context.Response.StatusCode = 400;
                    context.Response.WriteAsync(GenerateResponseBody(ex.Code, ex.Message));
                    break;
                case ArgumentNullException ex:
                    context.Response.StatusCode = 400;
                    context.Response.WriteAsync(GenerateResponseBody("ArgumentNull-400", ex.Message));
                    break;
                case ArgumentOutOfRangeException ex:
                    context.Response.StatusCode = 400;
                    context.Response.WriteAsync(GenerateResponseBody("ArgumentOutofRang-400", ex.Message));
                    break;
                default:
                    context.Response.StatusCode = 500;
                    context.Response.WriteAsync(GenerateResponseBody(
                        "InternalServerError_500",
                        "Something went wrong. Please contact your administrator."));
                    break;
            }
        }

        private string GenerateResponseBody(string code, string message)
        {
            var response = new BaseResponseDto(message, code);

            return JsonSerializer.Serialize(response, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}
