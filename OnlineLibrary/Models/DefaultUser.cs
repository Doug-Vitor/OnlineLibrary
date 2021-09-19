using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineLibrary.Models
{
    public class DefaultUser : Entity
    {
        public IdentityUser IdentityUser { get; set; }

        public DefaultUser()
        {
        }

        public DefaultUser(IdentityUser identityUser)
        {
            IdentityUser = identityUser;
        }
    }
}
