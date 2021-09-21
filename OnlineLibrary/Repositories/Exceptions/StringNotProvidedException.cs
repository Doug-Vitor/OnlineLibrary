using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Exceptions
{
    public class StringNotProvidedException : ApplicationException
    {
        public StringNotProvidedException(string message) : base(message)
        {
        }
    }
}
