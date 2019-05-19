using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Core.Interfaces
{
    interface ICustomModelMapping
    {
        void CreateMappings(Profile configuration);
    }
}
