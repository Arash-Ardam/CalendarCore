using DataStructures;
using System.ComponentModel;
using System.Data;

namespace CalendarDomain;
public class Calendar
{
    public string Name { get; set; }

    //public List<System.DayOfWeek> Weekend { get; private set; } = new List<DayOfWeek>();

    public AffectedByDateCollection<List<DayOfWeek>> Weekend { get; set; } = new AffectedByDateCollection<List<DayOfWeek>>();

    public List<DateEvent> Events { get; private set; } = new List<DateEvent>();


    #region ctor
    private Calendar() { }

    private Calendar(string name) { Name = name; }

    public static Calendar CreateByName(string name)
    {
        return new Calendar(name);
    }
    #endregion

    #region null pattern
    private static readonly Calendar _empty = new Calendar();
    public static Calendar Empty { get { return _empty; } }
    #endregion


    public void SetDefaultWeekend(DateTime affectedDate ,List<DayOfWeek> weekends)
    {
        Weekend.Add(affectedDate, weekends);
    }

    public void UnSetDefaultWeekend(DateTime affectedDate)
    {
        Weekend.Remove(affectedDate);
    }

    public void ModifyDefaultWeekend(DateTime affectedDate,List<DayOfWeek> weekends)
    {
        Weekend.Modify(affectedDate,weekends);
    }


    public void AddEvent(DateTime dateTime, string eventDescription, bool isHoliday)
    {
        var date = Events.FirstOrDefault(value => value.Date == dateTime && value.Description == eventDescription);

        if (default != date)
        {
            throw new DuplicateNameException();
        }

        Events.Add(new DateEvent() { Date = dateTime, Description = eventDescription, IsHoliday = isHoliday });
    }

    public void RemoveEvent(DateTime dateTime, string eventDescription)
    {
        var date = Events.FirstOrDefault(value => value.Date == dateTime && value.Description == eventDescription);

        if (default != date)
        {
            throw new DuplicateNameException();
        }

        Events.Remove(date);
    }


    public bool IsHoliday(DateTime dateTime)
    {
        var weekends = this.Weekend.Get(dateTime);

        if(weekends.Contains(dateTime.DayOfWeek))
        {
            return true;
        }

        return Events.Any(evt => evt.Date == dateTime && evt.IsHoliday);
            
    }

    public bool IsWorkingDay(DateTime dateTime)
    {
        return !IsHoliday(dateTime);
    }
}