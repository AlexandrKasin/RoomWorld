using System;

namespace Service.Exceptions
{
    public class IncorrectAuthParamsException : Exception
    {
        public IncorrectAuthParamsException()
        {
        }

        public IncorrectAuthParamsException(string message) : base(message)
        {
        }

        public IncorrectAuthParamsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}