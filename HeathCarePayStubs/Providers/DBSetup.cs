using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBConnections;
using System.Data;

namespace HeathCarePayStubs.Providers
{
    public class DBSetup        
    {
        public static DBConnections.IDBShell Shell { get; set; }
        public Boolean SetupDatabase(String configPath)
        {
            if (configPath.Length > 0)
            {
                Shell = new DBShell();
                Shell.SetConfigurationLocation(configPath);
            }
            return true;
        }

    }
}