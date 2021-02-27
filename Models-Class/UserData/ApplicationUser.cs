using Microsoft.AspNetCore.Identity;
using System;

namespace Projeto_Rumos.Areas.Identity.Pages.Account.UserData
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string Surname { get; set; }
        public virtual string StreetAddress { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
    }
}
