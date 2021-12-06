using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middileware
{
    public class ExceptionMiddileware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddileware> logger;
        private readonly IHostEnvironment hostEnvironment;

        public ExceptionMiddileware(RequestDelegate next,
            ILogger<ExceptionMiddileware> logger,
            IHostEnvironment hostEnvironment)
        {
            this.next = next;
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = hostEnvironment.IsDevelopment()
                                ? new ApiException(httpContext.Response.StatusCode, ex.Message, ex.StackTrace)
                                : new ApiException(httpContext.Response.StatusCode);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
