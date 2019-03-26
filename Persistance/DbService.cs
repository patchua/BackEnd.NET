using System;
using System.Collections.Generic;
using System.Text;
using Application;
using DevChallenge.Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class DbService : DbContext, IDBService
    {
        public DbSet<InstrumentPrice> InstrumentPrices { get ; set ; }

        public void Save()
        {
            this.SaveChangesAsync();
        }
        public DbService() : base()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new SaleConfiguration());
        }
    }
}
