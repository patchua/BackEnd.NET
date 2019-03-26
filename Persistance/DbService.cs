using System;
using System.Collections.Generic;
using System.Text;
using Application;
using DevChallenge.Domain;
using Microsoft.EntityFrameworkCore;
using Persistance.InstrumentPrices;

namespace Persistance
{
    public class DbService : DbContext, IDBService
    {
        public DbService(DbContextOptions<DbService> options) : base(options)
        {
        }
        public DbSet<InstrumentPrice> InstrumentPrices { get ; set ; }

        public async void SaveAsync()
        {
            await this.SaveChangesAsync();
        }

        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new InstrumentPriceConfiguration());
        }
    }
}
