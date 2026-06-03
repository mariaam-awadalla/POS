
using Microsoft.EntityFrameworkCore;
using POS.API.Models;
using System.Collections.Generic;

namespace POS.API.Data
{
    

    namespace UserAPI.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext
            (
                DbContextOptions<ApplicationDbContext> options
            ) : base(options)
            {

            }

            public DbSet<User> Users { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<User>().HasData(

                 new User
                 {
                     Id = 1,
                     Name = "Menna Ahmed",
                     Age = 22,
                     Gender = "Female",
                     Email = "menna@gmail.com",
                     CreatedAt = new DateTime(2026, 6, 3)
                 },

                 new User
                 {
                     Id = 2,
                     Name = "Ahmed Ali",
                     Age = 25,
                     Gender = "Male",
                     Email = "ahmed@gmail.com",
                     CreatedAt = new DateTime(2026, 6, 3)
                 },

                 new User
                 {
                     Id = 3,
                     Name = "Sara Mohamed",
                     Age = 24,
                     Gender = "Female",
                     Email = "sara@gmail.com",
                     CreatedAt = new DateTime(2026, 6, 3)
                 }
             );

               
            }
        }
    }
}
