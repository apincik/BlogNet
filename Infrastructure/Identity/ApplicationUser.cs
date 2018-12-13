using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string GetEmail()
        {
            return Email;
        }

        public string GetUserName()
        {
            return UserName;
        }

        public Guid GetGuid()
        {
            return new Guid(Id);
        }
    }
}
