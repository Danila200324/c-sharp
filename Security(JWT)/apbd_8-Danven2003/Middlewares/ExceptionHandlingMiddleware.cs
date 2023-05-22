namespace Zadanie8.Middlewares;

using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _logFilePath = "C:\\APBD\\APBD9\\apbd_8-Danven2003\\logs.txt";

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

    private void LogError(Exception ex)
    {
        string logMessage = $"{DateTime.UtcNow} {ex.Message}";

        using (var writer = new StreamWriter(_logFilePath, true))
        {
            writer.WriteLine(logMessage);
        }
    }
}
