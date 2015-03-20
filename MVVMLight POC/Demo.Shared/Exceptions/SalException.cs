using System;

namespace Demo.Shared.Exceptions
{
    public class SalException : Exception
    {
        public SalException()
        {
        }

        public SalException(string message) : base(message)
        {
        }

        public SalException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}