using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        IApplicationUserManager _appUserManager;

        public ApplicationUserService(IApplicationUserManager appUserManager)
        {
            _appUserManager = appUserManager;
        }
    }
}
