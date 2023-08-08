using System;

namespace SearchAndMatch.Api.Exceptions
{
    public class NotFoundException : Exception
    {
        protected NotFoundException()
        {
        }

        public NotFoundException(string message) : this(null, message){

        }

        public NotFoundException(Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }
} 