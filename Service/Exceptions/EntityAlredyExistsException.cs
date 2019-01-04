using System;

namespace Service.Exceptions
{
    public class EntityAlredyExistsException : Exception
    {
        public EntityAlredyExistsException()
        {
        }

        public EntityAlredyExistsException(string message) : base(message)
        {
        }

        public EntityAlredyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}