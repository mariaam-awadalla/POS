
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
        }
    }
}
