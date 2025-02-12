using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Plushing.Data
{
    public class PlushingContext : IdentityDbContext
    {
        public PlushingContext(DbContextOptions<PlushingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
