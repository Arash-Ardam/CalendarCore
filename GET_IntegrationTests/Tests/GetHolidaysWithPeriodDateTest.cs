
namespace GET_IntegrationTests.Tests
{
    [Order(4)]
    public class GetHolidaysWithPeriodDateTest : IntegrationTestBase
    {
        public GetHolidaysWithPeriodDateTest(CalendarWebApplicationFactory factory) : base(factory)
        {
        }

        [Theory,Order(1)]
        [InlineData("2022-09-18", "2022-09-24")]
        public async Task GetHolidaysWithPeriodDate_Should_Return_True_Result(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/HolidaysWithPeriodDate/{startDate}/{endDate}");

            //Act
            var response = await _httpClient.GetAsync(route);

            var content = await response.Content.ReadAsStringAsync();
            var actual = Newtonsoft.Json.JsonConvert.DeserializeObject(content);


            var path = Path.Combine(Environment.CurrentDirectory, "Tests", "GetHolidaysWithPeriodDate_Expexted_item.json");
            var json = File.ReadAllText(path);
            var expected = Newtonsoft.Json.JsonConvert.DeserializeObject(json);


            //Assert
            Assert.Equal(expected, actual);

        }

        [Theory,Order(2)]
        [InlineData("2019-05-05", "2019-09-12")]
        [InlineData("2023-02-01", "2023-02-06")]
        [InlineData("2023-03-25", "2042-06-08")]
        public async Task GetHolidaysWithPeriodDate_Should_Return_OK(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/HolidaysWithPeriodDate/{startDate}/{endDate}");
            
            //Act 
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory,Order(3)]
        [InlineData("ananas", "moz")]
        [InlineData("2022-00-15","2022-05-05")]
        public async Task GetHolidaysWithPeriodDate_Should_Return_BadRequest(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/HolidaysWithPeriodDate/{startDate}/{endDate}");

            //Act
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
        }

        [Theory,Order(5)]
        [InlineData("2020/05/05", "2020/06/12")]
        [InlineData("2020-05/15", "2020-06-09")]
        [InlineData("2019-05-06", "")]
        public async Task GetHolidaysWithPeriodDate_Should_Return_NotFound(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/HolidaysWithPeriodDate/{startDate}/{endDate}");

            //Act
            var responce = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, responce.StatusCode);
        }

        [Theory,Order(4)]
        [InlineData("2015-05-06", "2017-06-09")]
        public async Task GetHolidaysWithPeriodDate_Should_Return_InternalServerError(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/HolidaysWithPeriodDate/{startDate}/{endDate}");

            //Act
            var responce = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, responce.StatusCode);
        }
    }
}
