
namespace GET_IntegrationTests.Tests
{
    [Order(9)]
    public class WorkingDayCountTest : IntegrationTestBase
    {
        public WorkingDayCountTest(CalendarWebApplicationFactory factory) : base(factory)
        {
        }

        [Theory,Order(1)]
        [InlineData("2019-01-10", "2019-01-16")]
        public async Task WorkingDayCount_Should_Return_True_Result(string startDate, string endDate)
        {
            //Arrange 
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/WorkingDayCount/{startDate}/{endDate}");

            //Act
            var responce = await _httpClient.GetAsync(route);

            var differenceDate = DateTime.Parse(endDate) - DateTime.Parse(startDate);
            var diffNum = differenceDate.TotalDays - 1; // WorkingTimeSpan minus one existing holiday

            var content = await responce.Content.ReadAsStringAsync();
            var JsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

            //Assert
            Assert.Equal(Convert.ToInt64(diffNum), JsonResult);
        }

        [Theory,Order(2)]
        [InlineData("2019-05-05", "2019-09-12")]
        [InlineData("2023-02-01", "2023-02-06")]
        [InlineData("2023-03-25", "2042-06-08")]
        public async Task WorkingDayCount_Should_Return_Ok(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/WorkingDaycount/{startDate}/{endDate}");

            //Act 
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory,Order(3)]
        [InlineData("ananas", "moz")]
        [InlineData("2019-05-06", "200")]
        [InlineData("2022-00-15", "2022-05-05")]
        public async Task WorkingDayCount_Should_Return_BadRequest(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/WorkingDaycount/{startDate}/{endDate}");

            //Act
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory,Order(5)]
        [InlineData("2020-05/15", "2020-06-09")]
        [InlineData("2020/05/05", "2020/06/12")]
        public async Task WorkingDayCount_Should_Return_Notfound(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/WorkingDaycount/{startDate}/{endDate}");

            //Act
            var response = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory,Order(4)]
        [InlineData("2015-05-06", "2017-06-09")]
        public async Task WorkingDayCount_Should_Return_InternalServerError(string startDate, string endDate)
        {
            //Arrange
            Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/WorkingDaycount/{startDate}/{endDate}");

            //Act
            var responce = await _httpClient.GetAsync(route);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, responce.StatusCode);
        }

    }
}
