using System;

namespace DevChallenge.Domain
{
    public class InstrumentPrice
    {
        public InstrumentPrice(Portfolio portfolio, InstrumentOwner owner, Instrument instrument, TimeSlot date, decimal price)
        {
            Portfolio = portfolio ?? throw new ArgumentNullException(nameof(portfolio));
            InstrumentOwner = owner ?? throw new ArgumentNullException(nameof(owner));
            Instrument = instrument ?? throw new ArgumentNullException(nameof(instrument));
            Date = date ?? throw new ArgumentNullException(nameof(date));
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
            Price = price;
        }

        public Portfolio Portfolio { get; }
        public InstrumentOwner InstrumentOwner { get; }
        public Instrument Instrument { get; }
        public TimeSlot Date { get; }
        public decimal Price { get; }
    }
}