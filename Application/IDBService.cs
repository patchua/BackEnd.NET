using DevChallenge.Domain;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public interface IDBService
    {
        DbSet<InstrumentPrice> InstrumentPrices { get; set; }
        void Save();
    }
}