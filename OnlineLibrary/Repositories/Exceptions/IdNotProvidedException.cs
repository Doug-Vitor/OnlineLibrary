using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Exceptions
{
    public class IdNotProvidedException : ApplicationException
    {
        public IdNotProvidedException(string message) : base(message)
        {
        }
    }
}
