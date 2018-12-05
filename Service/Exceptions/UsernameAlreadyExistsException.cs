using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException()
        {
        }

        public UsernameAlreadyExistsException(string message) : base(message)
        {
        }

        public UsernameAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}