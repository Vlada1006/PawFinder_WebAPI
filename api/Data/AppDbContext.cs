using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LostPet> LostPets { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LostPet>()
            .HasMany(u => u.Comments)
            .WithOne(u => u.LostPet)
            .HasForeignKey(u => u.PetId)
            .OnDelete(DeleteBehavior.Cascade);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}