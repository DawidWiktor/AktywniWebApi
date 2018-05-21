using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Aktywni.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
   /*         string currentProjectPath = Environment.CurrentDirectory;
            var host = new WebHostBuilder()
    .UseKestrel(options =>
    {
        options.Listen(IPAddress.Parse("127.0.0.1"), 443, listenOptions => 
        {
            listenOptions.UseHttps(currentProjectPath + "\\certificate.pfx", "poklikasz1");
        });
    })
    .UseStartup<Startup>()
    .Build();
    host.Run();*/
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
