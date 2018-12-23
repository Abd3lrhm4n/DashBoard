using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MyContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyContext>(null);
            modelBuilder.Entity<Item>().Property(p => p.Price).HasPrecision(9, 2);

            //make storeProcedures
            modelBuilder.Entity<Item>().MapToStoredProcedures();

            base.OnModelCreating(modelBuilder);
        }
    }
}
