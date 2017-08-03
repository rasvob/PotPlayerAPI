using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;

namespace PotPlayerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:56111;http://192.168.2.7:56111")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();

            //bool isService = !(Debugger.IsAttached || args.Contains("--console"));

            //foreach (string s in args)
            //{
            //    Console.WriteLine(s);
            //}

            //var pathToContentRoot = Directory.GetCurrentDirectory();
            //if (isService)
            //{
            //    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            //    pathToContentRoot = Path.GetDirectoryName(pathToExe);
            //}

            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseUrls("http://localhost:56111;http://192.168.2.7:56111")
            //    .UseContentRoot(pathToContentRoot)
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .UseApplicationInsights()
            //    .Build();

            //if (isService)
            //{
            //    host.RunAsService();
            //}
            //else
            //{
            //    host.Run();
            //}
        }
    }
}
