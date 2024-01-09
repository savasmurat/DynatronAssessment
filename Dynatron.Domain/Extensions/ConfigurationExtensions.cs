using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Domain.Extensions
{
    public static class IConfigurationExtensions
    {
        public static string GetRequiredValue(this IConfiguration configuration, string key)
        {
            var configurationValue = configuration[key];

            if (configurationValue == null)
            {
                throw new Exception($"Missing IConfiguration value: {key}");
            }
            else if (string.IsNullOrWhiteSpace(configurationValue))
            {
                throw new Exception($"Empty IConfiguration value: {key}");
            }

            return configurationValue;
        }
    }
}
