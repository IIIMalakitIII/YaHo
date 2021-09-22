using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using YaHo.YaHoApiService.Common.Exceptions;

namespace YaHo.YaHoApiService.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case BusinessLogicException ex:
                    await WriteErrorAsync(context, ex.Message, HttpStatusCode.BadRequest);
                    _logger.LogError(exception, ex.Message);
                    break;

                case DataLogicException ex:
                    await WriteErrorAsync(context, ex.Message, HttpStatusCode.InternalServerError);
                    _logger.LogError(exception, ex.Message);
                    break;

                case DbUpdateException ex:
                    await WriteErrorAsync(context, "Failed to update in DataBase", HttpStatusCode.InternalServerError);
                    _logger.LogError(exception, ex.Message);
                    break;

                default:
                    await WriteErrorAsync(context, @"Something went wrong ¯\_(ツ)_/¯, try again later", HttpStatusCode.InternalServerError);
                    _logger.LogError(exception, "Internal server error");
                    break;
            }
        }

        
        private async Task WriteErrorAsync(HttpContext context, string error, HttpStatusCode statusCode)
        {
            var serializedError = JsonConvert.SerializeObject(new { error });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(serializedError);
        }
    }
}
