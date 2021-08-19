using System;

namespace Task3.Exceptions
{
    public class UserTaskAlreadyExistsException : Exception
    {
        public UserTaskAlreadyExistsException() { }
        public UserTaskAlreadyExistsException(string message) : base(message) { }
        public UserTaskAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
