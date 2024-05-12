using CalendarApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GET_IntegrationTests.Absractions
{
    public class CalendarWebApplicationFactory : WebApplicationFactory<Program>
    {

        private readonly string _testEnviroment = "test";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment(_testEnviroment);
        }

    }
}
