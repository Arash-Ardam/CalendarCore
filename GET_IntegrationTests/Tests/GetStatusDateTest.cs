
namespace GET_IntegrationTests.Tests
{
    [Order(5)]
    public class GetStatusDateTest : IntegrationTestBase
    {
        public GetStatusDateTest(CalendarWebApplicationFactory factory) : base(factory)
        {
        }

        [Theory,Order(1)]
        [InlineData("2022-05-05")]
        [InlineData("2019-02-02")]
        [InlineData("2020-05")]
        public async Task GetStatusDate_Should_Return_OK(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/StatusDate/{date}");

            //Act
            HttpResponseMessage responce = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
        }

        [Theory,Order(2)]
        [InlineData("202")]
        [InlineData("ananas")]
        public async Task GetStatusDate_Should_Return_BadRequest(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/StatusDate/{date}");

            //Act
            HttpResponseMessage responce = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
        }

        [Theory,Order(5)]
        [InlineData("2020/05/06")]
        [InlineData("2020-05/15")]
        [InlineData("")]
        public async Task GetStatusDate_Should_Return_NotFound(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/StatusDate/{date}");

            //Act
            HttpResponseMessage responce = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, responce.StatusCode);
        }

        [Theory,Order(3)]
        [InlineData("2015-05-15")]
        public async Task GetStatusDate_Should_Return_InternalServerError(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/StatusDate/{date}");

            //Act
            HttpResponseMessage responce = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, responce.StatusCode);
        }

        [Theory,Order(4)]
        [InlineData("2019-01-13")]
        public async Task GetStatusDate_Should_Return_True_Result(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/StatusDate/{date}");

            //Act
            var responce = await _httpClient.GetAsync(route);

            var content = await responce.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

            var expected = new DateTime(2019, 01, 13);

            //Assert
            Assert.Equal(expected, result);

        }
    }
}
