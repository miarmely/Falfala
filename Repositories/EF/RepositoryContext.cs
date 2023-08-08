using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Repositories.EF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EF
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }


        public RepositoryContext(DbContextOptions options) : base(options)
        {}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LoginConfig());
        }
    }
}
