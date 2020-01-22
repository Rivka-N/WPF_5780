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

    public class unfoundRequestExceptionBL:Exception//only empty list to return (where error is relevent)
        {}
}