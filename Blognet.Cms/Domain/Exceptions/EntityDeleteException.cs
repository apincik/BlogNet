using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Domain.Exceptions
{
    public class EntityDeleteException : Exception
    {
        public EntityDeleteException(string message) : base(message)
        {

        }
    }
}
