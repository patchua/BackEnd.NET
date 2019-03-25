using System;
using System.Collections.Generic;
using System.Text;

namespace Application.InstrumentPrices.Queries
{
    public class AveragePriceDto
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
