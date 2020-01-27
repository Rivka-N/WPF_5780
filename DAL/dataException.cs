using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    internal class dataException : Exception
    {
        public dataException()
        {
        }

        public dataException(string message) : base(message)
        {
        }

        public dataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected dataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    class loadExceptionDAL : Exception//loading to and from xml files error
    {
        public loadExceptionDAL(string message) : base(message)
        {
        }
    }
    class objectErrorDAL:Exception//error in finding or accessing object
    { }

    class duplicateErrorDAL:Exception//trying to use create duplicate information
    { }
    class notDownloadedException:Exception//didn't finish downloading
    { }
}