using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynatron.Infrastructure.Interfaces
{
    public interface IAppSettings
    {
        string CorsOrigins { get; }
    }
}
