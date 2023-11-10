using System.Diagnostics;
using System.Net;
using System.Xml;

namespace Vk.Api.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate next;
    
    // next bir sonraki middleware'ı tutar
    public CustomExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        try
        {
            // Request Section
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);
        
            await next.Invoke(context);
            watch.Stop();

            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + "responded" + context.Response.StatusCode + "in" + watch.Elapsed.TotalMilliseconds + "ms";
            Console.WriteLine(message);     
        }
        catch (Exception ex)
        {
            watch.Stop();
            await HandleException(context, ex, watch);
        }
        

    }

    private Task HandleException(HttpContext context, Exception exception, Stopwatch watch)
    {
        string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode +
                         "Error Message" + exception.Message + "in" + watch.Elapsed.TotalMilliseconds + "ms";
        Console.WriteLine(message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        var result = JsonConvert.SerializeObject(new { error = exception.Message }, Formatting.None);
        
        return context.Response.WriteAsync(result);
    }
}

internal class JsonConvert
{
    public static string SerializeObject(object o, object none)
    {
        throw new NotImplementedException();
    }
}

static public class CustomExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExcepciton(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}