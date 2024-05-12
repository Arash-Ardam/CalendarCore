namespace CalendarDomain.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Calendar_isHoliday_When_holidayEvent_Added()
        {
            Calendar calendar = setTestCalendar();

            var isHoliday = calendar.IsHoliday(new DateTime(2024, 04, 13));

            Assert.True(isHoliday);
        }



        [Fact]
        public void Test2()
        {
            Calendar calendar = setTestCalendar();
            var date = new DateTime(2024, 04, 13);

            var isHoliday = calendar.IsWorkingDay(new DateTime(2024, 04, 13));

            Assert.False(isHoliday);
        }

        [Fact]
        public void Test3()
        {
            Calendar calendar = setTestCalendar();
            var date = new DateTime(2024, 04, 13);

            var isHoliday = calendar.IsHoliday(new DateTime(2024, 04, 13).AddDays(1));

            Assert.False(isHoliday);
        }

        [Fact]
        public void Test4()
        {
            Calendar calendar = setTestCalendar();
            var date = new DateTime(2024, 04, 13);
     

            var isHoliday = calendar.IsWorkingDay(new DateTime(2024, 04, 13).AddDays(1));

            Assert.True(isHoliday);
        }

        [Fact]
        public void SetWeekend_Should_work_when_newCalendar_created()
        {
            //Arrange
            var newCalendar = Calendar.CreateByName("test");
            var weekends = new List<DayOfWeek>() { DayOfWeek.Friday, DayOfWeek.Saturday };
            DateTime affectedDate = new DateTime(2024 , 04, 16);
            var isDone = false;

            List<DayOfWeek> enteredWeekends = default;

            //Act
            try
            {
                newCalendar.SetDefaultWeekend(affectedDate, weekends);
                isDone = true;
                enteredWeekends = newCalendar.Weekend.Get(affectedDate);
            }
            catch (Exception)
            {
                throw;
            }
            

            //Assert
            Assert.True(isDone);
            Assert.All(weekends, w => enteredWeekends.Any(e => e == w));
        }


        private static Calendar setTestCalendar()
        {
            Calendar calendar = Calendar.CreateByName("test");
            var weekends = new List<DayOfWeek>() { DayOfWeek.Friday, DayOfWeek.Saturday };

            calendar.SetDefaultWeekend(new DateTime(2023, 01, 01), weekends);
            
            var date = new DateTime(2024, 04, 13);

            calendar.AddEvent(date, "Test Holiday", true);
            return calendar;
        }


    }
}










// calendar.SetDefaultWeekend(affectedDate,weekends) => Done

