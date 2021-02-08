using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projeto_Rumos.Areas.Identity.Pages.Account.UserData
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string Surname { get; set; }
        public virtual string StreetAddress { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
    }
}
