using System.Security.Claims;

using Serilog.Context;

namespace TodoApi.Middleware;

public class LoggingMiddleware
{
    private const string HeaderName = "X-Correlation-ID";
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext ctx)
    {
        var correlationId = ctx.Request.Headers.TryGetValue("X-Correlation-ID", out var cid)
       ? cid.ToString()
       : Guid.NewGuid().ToString();

        using (LogContext.PushProperty("RequestId", ctx.TraceIdentifier))
        using (LogContext.PushProperty("CorrelationId", correlationId))
        using (LogContext.PushProperty("UserAgent", ctx.Request.Headers["User-Agent"].ToString()))
        using (LogContext.PushProperty("ClientIP", ctx.Connection.RemoteIpAddress?.ToString()))
        {
            await _next(ctx);
        }
    }
}
