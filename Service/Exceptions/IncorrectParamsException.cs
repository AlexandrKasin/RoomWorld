using System;

namespace Service.Exceptions
{
    public class IncorrectParamsException : Exception
    {
        public IncorrectParamsException()
        {
        }

        public IncorrectParamsException(string message) : base(message)
        {
        }

        public IncorrectParamsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}