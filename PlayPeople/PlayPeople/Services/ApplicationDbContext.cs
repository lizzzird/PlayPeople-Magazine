using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayPeople.Models;

namespace PlayPeople.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var editor = new IdentityRole("editor");
            editor.NormalizedName = "editor";

            var reader = new IdentityRole("reader");
            reader.NormalizedName = "reader";

            builder.Entity<IdentityRole>().HasData(admin, editor, reader);
        }

        public DbSet<Game> Games { get; set; }
    }
}
