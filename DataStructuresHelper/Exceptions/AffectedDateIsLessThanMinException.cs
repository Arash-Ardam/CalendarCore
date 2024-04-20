using DataStructures.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Exceptions
{
    public class AffectedDateIsLessThanMinException : DomainException
    {

        public static string ExceptionTemplate = " The {0} Entered is less than {1}.";

        public AffectedDateIsLessThanMinException()
        {
        }

        public AffectedDateIsLessThanMinException(DateTime enteredDate, DateTime minSupported) : base(string.Format(ExceptionTemplate, enteredDate, minSupported))
        {
        }

        //public WeekendKeyOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
        //{
        //}

        //protected WeekendKeyOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}
