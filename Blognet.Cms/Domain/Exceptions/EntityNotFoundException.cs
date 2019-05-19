using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string nameOfEntity, string id) : base($"{nameOfEntity} entity with Id {id} not found.")
        {

        }

        public EntityNotFoundException(string nameOfEntity, int id) : base($"{nameOfEntity} entity with Id {id.ToString()} not found.")
        {

        }
    }
}
