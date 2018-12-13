using Core.Entities;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class LoggedUserViewModel
    {
        public ApplicationUser User { get; set; }
        public Project Project { get; set; }
    }
}
