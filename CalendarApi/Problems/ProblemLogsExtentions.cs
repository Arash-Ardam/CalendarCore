using CalendarDomain.Exceptions.Calendar;
using DataStructures.Exceptions;

namespace CalendarRestApi.Problems
{
    public static class ProblemLogsExtentions
    {
            public static void AffectedByDateLogError(this AffectedDateIsLessThanMinException ex, ILogger logger)
            {
                logger.LogError(ex.Message);
            }

            public static void CalendarLogError(this CalendarNotFoundException ex, ILogger logger)
            {
                logger.LogError(ex.Message);

            }

            public static void EventLogError(this EventNotFoundException ex, ILogger logger)
            {
                logger.LogError(ex.Message);
            }

           

    }
}
