﻿using System;
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
}