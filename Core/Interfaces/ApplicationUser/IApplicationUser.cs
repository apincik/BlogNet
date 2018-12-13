using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IApplicationUser
    {
        string GetEmail();
        string GetUserName();
        Guid GetGuid();

    }
}
