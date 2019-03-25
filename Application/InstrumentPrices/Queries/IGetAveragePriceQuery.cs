using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstrumentPrices.Queries
{
    public interface IGetAveragePriceQuery
    {
       Task<AveragePriceDto> ExecuteAsync(DateTime time, string instrument, string owner, string portfolio);
    }
}
