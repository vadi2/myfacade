using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using myfacade.Models;
using Vonk.Core.Pluggability;
using Vonk.Core.Repository;

namespace myfacade
{
  [VonkConfiguration(order: 240)]
  public static class ViSiConfiguration
  {
    public static IServiceCollection AddViSiServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ViSiContext>();
      services.TryAddSingleton<ResourceMapper>();
      services.TryAddScoped<ISearchRepository, ViSiRepository>();

      services.Configure<DbOptions>(configuration.GetSection(nameof(DbOptions)));

      TelemetryConfiguration.Active.DisableTelemetry = true;
      TelemetryDebugWriter.IsTracingDisabled = true;

      return services;
    }
  }
}
