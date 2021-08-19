using System;

namespace Task3.Exceptions
{
    public class UserTaskNotMatchException : Exception
    {
        public UserTaskNotMatchException() { }
        public UserTaskNotMatchException(string message) : base(message) { }
        public UserTaskNotMatchException(string message, Exception inner) : base(message, inner) { }
    }
}
