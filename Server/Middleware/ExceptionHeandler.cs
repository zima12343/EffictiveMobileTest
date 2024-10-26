using System.Net;
using System.Text.Json;

namespace Server.Middleware
{
    public class ExceptionHeandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHeandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    error = new
                    {
                        message = ex.Message,
                    }
                }));
            }
        }
    }
}
