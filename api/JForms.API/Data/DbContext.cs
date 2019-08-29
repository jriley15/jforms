using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JForms.API.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {



        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }
    


    }
}
