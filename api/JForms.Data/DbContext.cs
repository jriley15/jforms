using JForms.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public DbSet<Form> Forms { get; set; }

        public DbSet<User> Users { get; set; }


        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }
    


    }
}
