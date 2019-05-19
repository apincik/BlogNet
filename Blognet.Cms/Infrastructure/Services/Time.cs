using Blognet.Cms.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blognet.Cms.Infrastructure.Services
{
    public class Time : ITime
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
