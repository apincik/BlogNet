using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class Time : ITime
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
