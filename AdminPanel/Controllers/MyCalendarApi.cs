
namespace AdminPanel.Controllers
{
    public class MyCalendarApi : CalendarApi
    {
        public MyCalendarApi(HttpClient httpClient) : base(httpClient.BaseAddress.ToString(), httpClient)
        {
        }

    }
}
