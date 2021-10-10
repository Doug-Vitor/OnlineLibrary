using System;

namespace OnlineLibrary.Repositories.Exceptions
{
    public class AccessDeniedException : ApplicationException
    {
        public AccessDeniedException(string message = null) : base(message)
        {
        }
    }
}
