using System;
using System.Runtime.Serialization;

namespace BL
{
    [Serializable]
    public class InvalidException : Exception
    {
        public InvalidException()
        {

        }

        public InvalidException(string message) : base(message)
        {
        }

        public InvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class invalidFormatBL : Exception//item isn't in the expceted format
    { }
    public class networkErrorExceptionBL : Exception//error in accessing server or sending mail
    {
        public networkErrorExceptionBL(string message) : base(message)
        {
        }
    }
    public class unfoundRequestExceptionBL:Exception//only empty list to return (where error is relevent)
        {}
    public class overbookedExceptionBL:Exception//no available dates
    { }
}