using Microsoft.AspNet.Identity.EntityFramework;
using AndroidManagerApplication.Models.Entities;

namespace AndroidManagerApplication.Models.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("UserDBConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}