using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HeathCarePayStubs.Startup))]

namespace HeathCarePayStubs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Providers.DBSetup dB = new Providers.DBSetup();
            dB.SetupDatabase(System.AppContext.BaseDirectory + "config.xml");


            ConfigureAuth(app);

        }
    }

}
