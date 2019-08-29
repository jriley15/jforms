using JForms.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Form> Forms { get; set; }

        public DbSet<User> Users { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    


    }
}
