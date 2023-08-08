using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SearchAndMatch.Api.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();
            var statusCode = exception switch
            {
                NotFoundException _ when exceptionType == typeof(NotFoundException) => HttpStatusCode.NotFound,
                BadHttpRequestException _ when exceptionType == typeof(BadHttpRequestException) => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };
            var errorMessage = statusCode switch
            {
                HttpStatusCode.InternalServerError => "Internal Server Error",
                HttpStatusCode.Unauthorized => "error",
                _ => exception.Message,
            };
            var response = new
            {
                code = statusCode,
                message = errorMessage
            };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);

        }
    }
}