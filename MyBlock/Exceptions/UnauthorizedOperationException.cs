using System;

namespace MyBlock.Exceptions
{
    public class UnauthorizedOperationException : ApplicationException
    {
        public UnauthorizedOperationException(string message) 
            : base(message)
        {
        }

    }
}
