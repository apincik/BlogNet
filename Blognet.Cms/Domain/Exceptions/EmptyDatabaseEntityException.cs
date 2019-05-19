using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Domain.Exceptions
{
    public class EmptyDatabaseEntityException : Exception
    {
        public EmptyDatabaseEntityException(string message) : base(message)
        {

        }
    }
}
