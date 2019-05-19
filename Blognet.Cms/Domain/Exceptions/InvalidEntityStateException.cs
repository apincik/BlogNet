using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Domain.Exceptions
{
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException(string message) : base(message)
        {

        }
    }
}
