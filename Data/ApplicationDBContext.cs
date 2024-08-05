using FinShark.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Data
{
    public class ApplicationDBContext: IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks {get; set;}
        public DbSet<Comment> Comments {get; set;}

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);


            List<IdentityRole> roles = [
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },

                new IdentityRole {
                    Name = "User",
                    NormalizedName = "USER",
                },
            ];

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}