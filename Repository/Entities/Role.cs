using Microsoft.AspNetCore.Identity;

namespace Repository.Entities
{
    public class Role: IdentityRole<Guid>
    {
        public string Description { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
