using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CloudDownloader
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Environment.Is64BitProcess
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development") env = "Debug";
            if (string.IsNullOrWhiteSpace(env)) env = "Debug";
            var configfile = $"appsettings.{env}.json";
            CreateHostBuilder(args, configfile).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="configfile"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args, string configfile)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(configfile, optional: true)
            .Build();
            //var configfile = File.ReadAllText($"appsettings.{env}.json").ParseJSON<Config>();

            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseKestrel();
                //webBuilder.UseIIS();
                webBuilder.UseConfiguration(config);
            });
        }
    }
}
