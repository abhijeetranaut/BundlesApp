using BundlesApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace BundlesApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Provide parts inventory as a dependency for InventoryService
                    var partsInventory = new Dictionary<string, int>
                    {
                        { "seat", 50 },
                        { "pedal", 60 },
                        { "frame", 60 },
                        { "tube", 35 }
                    };

                    // Register the parts inventory as a singleton service
                    services.AddSingleton(partsInventory);

                    // Register InventoryService with its dependencies
                    services.AddSingleton<InventoryService>();

                    // Register other services as needed
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
