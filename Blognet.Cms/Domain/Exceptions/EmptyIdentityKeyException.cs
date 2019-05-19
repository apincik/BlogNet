using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Domain.Exceptions
{
    public class EmptyIdentityKeyException : Exception
    {
        public EmptyIdentityKeyException(string message) : base(message)
        {

        }
    }
}
