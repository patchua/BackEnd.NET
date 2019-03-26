using DevChallenge.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.InstrumentPrices
{
    internal class InstrumentPriceConfiguration : IEntityTypeConfiguration<InstrumentPrice>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<InstrumentPrice> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Instrument).IsRequired();
            builder.Property(c => c.InstrumentOwner).IsRequired();
            builder.Property(c => c.Portfolio).IsRequired();
            builder.HasIndex(c => c.TimeSlot);            
        }
    }
}
