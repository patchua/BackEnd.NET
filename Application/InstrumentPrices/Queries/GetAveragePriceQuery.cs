using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevChallenge.Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.InstrumentPrices.Queries
{
    public class GetAveragePriceQuery:IGetAveragePriceQuery
    {
        private readonly IDBService _dbService;
        public GetAveragePriceQuery(IDBService dbService)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
        }

        public async Task<AveragePriceDto>  ExecuteAsync(DateTime time,string instrument,string owner, string portfolio)
        {
            int timeSlot = TimeSlot.DateTimeToTimeSlot(time);
            IList<InstrumentPrice> pricesList = await _dbService.InstrumentPrices.Where(x =>
                x.TimeSlot == timeSlot &&
                x.Portfolio == portfolio &&
                x.Instrument == instrument &&
                x.InstrumentOwner == owner).ToListAsync();
            if(!pricesList.Any())
                return null;
            return new AveragePriceDto()
            {
                Price = pricesList.Sum(x => x.Price) / pricesList.Count,
                Date = TimeSlot.GetTimeSlotStartDate(timeSlot)
        };
        }
    }
}
