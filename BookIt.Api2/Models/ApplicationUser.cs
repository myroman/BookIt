using BookIt.Business.Models;

using Microsoft.AspNet.Identity.EntityFramework;

namespace BookIt.Api2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}