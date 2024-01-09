using Dynatron.Domain.Extensions;
using Dynatron.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Dynatron.Infrastructure.Configurations
{
    public class AppSettings: IAppSettings
    {
        private readonly IConfiguration Configuration;
        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string CorsOrigins => Configuration.GetRequiredValue("CorsOrigins");
    }
}
