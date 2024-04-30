using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GET_IntegrationTests.Absractions
{
    public abstract class IntegrationTestBase : IClassFixture<CalendarWebApplicationFactory>
    {
        #region Constructor and fields
        private readonly CalendarWebApplicationFactory _factory;
        protected readonly HttpClient _httpClient;
        protected IntegrationTestBase(CalendarWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }
        #endregion

        protected Uri CreateRoute(string route)
        {
            return new Uri($"{_factory.Server.BaseAddress}{route}");
        }


    }
}
