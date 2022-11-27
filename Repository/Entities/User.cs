using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public partial class User: IdentityUser<Guid>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
    
        public DateTime? CreatedAt { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
