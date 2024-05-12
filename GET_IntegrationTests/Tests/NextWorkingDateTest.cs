
namespace GET_IntegrationTests.Tests
{
    [Order(7)]
    public class NextWorkingDateTest : IntegrationTestBase
    {
        public NextWorkingDateTest(CalendarWebApplicationFactory factory) : base(factory)
        {
        }

        [Theory, Order(1)]
        [InlineData("2019-05-05")]
        [InlineData("2022-01-06")]
        [InlineData("2022-02")]
        public async Task NextWorkingDate_Should_Return_OK(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/NextWorkingDate/{date}");

            //Act
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory, Order(2)]
        [InlineData("ananas")]
        public async Task NextWorkingDate_Should_Return_BadRequest(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/NextWorkingDate/{date}");

            //Act
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory, Order(5)]
        [InlineData("2020-05/15")]
        [InlineData("2020/05/05")]
        [InlineData("")]
        public async Task NextWorkingDate_Should_Return_NotFound(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/NextWorkingDate/{date}");

            //Act
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory, Order(3)]
        [InlineData("2015-05-05")]
        public async Task NextWorkingDate_Should_Return_InternalServerError(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/NextWorkingDate/{date}");

            //Act
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Theory, Order(4)]
        [InlineData("2022-01-24")]
        public async Task NextWorkingDate_Should_Return_True_Result(string date)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/NextWorkingDate/{date}");

            //Act
            var response = await _httpClient.GetAsync(route);

            var content = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

            var expected = new DateTime(2022, 01, 25);

            //Assert
            Assert.Equal(expected, result);
        }

    }
}
