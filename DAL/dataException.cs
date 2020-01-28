using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    class loadExceptionDAL : Exception//loading to and from xml files error
    {
        public loadExceptionDAL()
        {
        }

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