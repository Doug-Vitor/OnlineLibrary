using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineLibrary.Models
{
    public class ApplicationUser : Entity
    {
        public IdentityUser IdentityUser { get; protected set; }
        public virtual ShoppingCart ShoppingCart { get; protected set; }
        public virtual List<Purchase> Purchases { get; protected set; }

        public ApplicationUser()
        {
        }

        public ApplicationUser(IdentityUser identityUser)
        {
            IdentityUser = identityUser;
        }
    }
}
