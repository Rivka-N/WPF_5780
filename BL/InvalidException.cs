using System;
using System.Runtime.Serialization;

namespace BL
{
    [Serializable]
    public class InvalidException : Exception
    {
        string from;
        public InvalidException()
        {
            from = "BL";
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
}