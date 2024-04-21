using DataStructures.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CalendarDomain.Exceptions.Calendar
{
    public class EventNotFoundException : DomainException
    {

        public static string MessageTemplate = "Event with date : ( {0} ) not found.";

        public DateTime DateOfEvent { get; private set; }

        public EventNotFoundException()
        {
        }

        public EventNotFoundException(DateTime date) : base(string.Format(MessageTemplate,date))
        {
            DateOfEvent = date;
        }

        //public EventNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        //{
        //}

        //protected EventNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}
