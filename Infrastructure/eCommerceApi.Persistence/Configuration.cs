using System;
using Microsoft.Extensions.Configuration;

namespace eCommerceApi.Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/eCommerceApi.API");
                configurationManager.SetBasePath(path);
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}