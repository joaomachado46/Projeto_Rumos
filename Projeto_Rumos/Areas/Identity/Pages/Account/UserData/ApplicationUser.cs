using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Rumos.Areas.Identity.Pages.Account.UserData
{
    public class ApplicationUser : IdentityUser
    {
        public string SobreNome { get; set; }
        public string StreetAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
