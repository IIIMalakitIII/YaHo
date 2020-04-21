using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace YaHo.YaHoApiService.Middleware
{
    public class RequestPerformanceMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly Stopwatch _timer;

        public RequestPerformanceMiddleware(RequestDelegate next, ILoggerFactory factory)
        {
            _next = next;
            _logger = factory.CreateLogger<RequestPerformanceMiddleware>();
            _timer = new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _timer.Restart();

            await _next.Invoke(context);

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning($"Long running Request '{context.Request.Path}' with time: {_timer.ElapsedMilliseconds} ms");
            }
            else
            {
                _logger.LogInformation($"Request '{context.Request.Path}' with time: {_timer.ElapsedMilliseconds} ms");
            }
        }
    }
}
