using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CurlycircleWebApi.Infrastructure
{
  public static class HostExtensions
  {
    public static IHost MigrateDatabase(this IHost host)
    {
      var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      if (environment != Environments.Development)
      {
        return host;
      }

      using (var scope = host.Services.CreateScope())
      using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
      {
        try
        {
          appContext.Database.Migrate();
        }
        catch (Exception e)
        {
          var services = scope.ServiceProvider;
          var logger = services.GetRequiredService<ILogger>();
          logger.LogError(e, "An error occured during running migrations.");
        }
      }
      return host;
    }
  }
}
