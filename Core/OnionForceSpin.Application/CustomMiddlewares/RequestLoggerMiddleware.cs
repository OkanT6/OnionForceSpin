using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.CustomMiddlewares
{
    public class RequestLoggerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            // 📝 1️⃣ Temel Bilgiler
            string path = httpContext.Request.Path;
            string method = httpContext.Request.Method;
            string userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            string ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            string queryString = httpContext.Request.QueryString.HasValue ? httpContext.Request.QueryString.Value : "Yok";

            // 📝 2️⃣ Header Bilgileri
            var headers = httpContext.Request.Headers.Select(header => $"{header.Key}: {header.Value}").ToList();

            // 📝 3️⃣ Log Mesajı
            Console.WriteLine("🌐 İstek Loglandı:");
            Console.WriteLine($"   → Path: {path}");
            Console.WriteLine($"   → Method: {method}");
            Console.WriteLine($"   → User-Agent: {userAgent}");
            Console.WriteLine($"   → IP Address: {ipAddress}");
            Console.WriteLine($"   → Query String: {queryString}");
            Console.WriteLine($"   → Headers: ");
            headers.ForEach(header => Console.WriteLine($"      - {header}"));

            await next(httpContext); // Bir sonraki middleware'e geçiş yap
        }
    }

}

/*
 // 1Path Kontrolü Yaparak Belirli İstekleri Loglama
//Örneğin login işlemi yaparken user-agent'ı dikkate alacağım


public class RequestLoggerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        // İstek path'ini al
        string path = httpContext.Request.Path;

        // Sadece /api/login ile gelen istekleri logla
        if (path.StartsWithSegments("/api/login"))
        {
            // 📝 1️⃣ Temel Bilgiler
            string method = httpContext.Request.Method;
            string userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            string ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            string queryString = httpContext.Request.QueryString.HasValue ? httpContext.Request.QueryString.Value : "Yok";

            // 📝 2️⃣ Header Bilgileri
            var headers = httpContext.Request.Headers.Select(header => $"{header.Key}: {header.Value}").ToList();
            
            // 📝 3️⃣ Log Mesajı
            Console.WriteLine("🌐 İstek Loglandı (Login):");
            Console.WriteLine($"   → Path: {path}");
            Console.WriteLine($"   → Method: {method}");
            Console.WriteLine($"   → User-Agent: {userAgent}");
            Console.WriteLine($"   → IP Address: {ipAddress}");
            Console.WriteLine($"   → Query String: {queryString}");
            Console.WriteLine($"   → Headers: ");
            headers.ForEach(header => Console.WriteLine($"      - {header}"));
        }

        await next(httpContext); // Bir sonraki middleware'e geçiş yap
    }
}


 */
