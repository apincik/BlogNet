using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Domain.Exceptions
{
    public class EntityStoreException : Exception
    {
        public EntityStoreException(string message) : base(message)
        {

        }
    }
}
