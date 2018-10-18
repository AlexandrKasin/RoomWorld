using System;

namespace Service.Exceptions
{
    public class FlatNotFoundException : Exception
    {
        public FlatNotFoundException()
        {
        }

        public FlatNotFoundException(string message) : base(message)
        {
        }

        public FlatNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}