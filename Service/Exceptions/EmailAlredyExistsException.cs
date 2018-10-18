using System;
using System.Runtime.Serialization;

namespace Service.Exceptions
{
    public class EmailAlredyExistsException : Exception
    {
        public EmailAlredyExistsException()
        {
        }

        public EmailAlredyExistsException(string message) : base(message)
        {
        }

        public EmailAlredyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}