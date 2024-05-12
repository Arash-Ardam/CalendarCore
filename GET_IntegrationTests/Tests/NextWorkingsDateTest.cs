
namespace GET_IntegrationTests.Tests;
[Order(8)]
public sealed class NextWorkingsDateTest : IntegrationTestBase
{

    #region Constructor
    public NextWorkingsDateTest(CalendarWebApplicationFactory factory) : base(factory)
    {
    }
    #endregion

    [Theory,Order(1)]
    [InlineData("2022-09-14",7)]
    public async Task GetNextWorkingsDate_Should_Return_True_Result(string date, int step)
    {
        //Arrange
        Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/GetNextWorkingsDate/{date}/{step}");

        //Act
        var response = await _httpClient.GetAsync(route);

        var content = await response.Content.ReadAsStringAsync();
        var actual = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

        var path = Path.Combine(Environment.CurrentDirectory, "Tests",
            "GetNextWorkingsDate_TrueResult_Entity.json");
        var json = File.ReadAllText(path);
        var expected = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

        //Assert
        Assert.Equal(expected, actual);

    }

    [Theory,Order(2)]
    [InlineData("2022-09-14", 7)]
    [InlineData("2022-08-14", 17)]
    [InlineData("2022-07", 20)]
    [InlineData("2022-06-14", 12)]
    public async Task GetNextWorkingsDate_Should_ReturnOK(string date, int step)
    {
        //Arrange
        Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/GetNextWorkingsDate/{date}/{step}");

        //Act
        HttpResponseMessage response = await _httpClient.GetAsync(route);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory,Order(3)]
    [InlineData("2015-05-05",12)] 
    public async Task GetNextWorkingDate_Should_Return_InternalServerError(string date, int step)
    {
        //Arrange
        Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/GetNextWorkingsDate/{date}/{step}");

        //Act
        HttpResponseMessage response = await _httpClient.GetAsync(route);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Theory,Order(4)]
    [InlineData("2022-07-14", 0)]
    [InlineData("2022-06-14", -10)]
    [InlineData("20",14)]
    [InlineData("ananas",2)]
    [InlineData("2022-00-15",12)]
    public async Task GetNextWorkingDate_Should_ReturnBadRequest(string date, int step)
    {
        //Arrange
        Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/GetNextWorkingsDate/{date}/{step}");

        //Act
        HttpResponseMessage response = await _httpClient.GetAsync(route);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory,Order(5)]
    [InlineData("2022/06/14", 12)]
    [InlineData("2022/07-14", 20)]
    [InlineData("", 1)]
    public async Task GetNextWorkingDate_Should_Return_NotFound(string date, int step)
    {
        //Arrange
        Uri route = CreateRoute($"calendar/DEFAULT_TRADING_SYSTEM/GetNextWorkingsDate/{date}/{step}");

        //Act
        HttpResponseMessage response = await _httpClient.GetAsync(route);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}