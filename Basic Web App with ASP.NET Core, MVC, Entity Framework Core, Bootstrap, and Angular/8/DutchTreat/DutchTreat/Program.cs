using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupConfiguration)
            .UseStartup<Startup>()
            .Build();

    private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
    {
      // Removing the default configuration options
      builder.Sources.Clear();

      builder.AddJsonFile("config.json", false, true)
             .AddEnvironmentVariables();
    }
  }
}
