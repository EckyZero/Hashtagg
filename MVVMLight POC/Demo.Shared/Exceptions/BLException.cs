using System;

namespace Demo.Shared.Exceptions
{
    public class BLException : BaseException
    {
        public BLException()
        {
        }

        public BLException(string message) : base(message)
        {
        }

        public BLException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}