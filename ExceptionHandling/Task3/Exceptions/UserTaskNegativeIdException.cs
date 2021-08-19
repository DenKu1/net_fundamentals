using System;

namespace Task3.Exceptions
{
    public class UserTaskNegativeIdException : Exception
    {
        public UserTaskNegativeIdException() { }
        public UserTaskNegativeIdException(string message) : base(message) { }
        public UserTaskNegativeIdException(string message, Exception inner) : base(message, inner) { }
    }
}
