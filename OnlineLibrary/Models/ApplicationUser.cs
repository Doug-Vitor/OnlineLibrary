using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineLibrary.Models
{
    public class ApplicationUser : Entity
    {
        public IdentityUser IdentityUser { get; set; }
        public virtual List<Purchase> Purchases { get; set; }

        public ApplicationUser()
        {
        }

        public ApplicationUser(IdentityUser identityUser)
        {
            IdentityUser = identityUser;
        }
    }
}
