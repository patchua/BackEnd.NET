using System;

namespace DevChallenge.Domain
{
    public class InstrumentPrice
    {
        public InstrumentPrice(string portfolio, string owner, string instrument, int timeSlot, decimal price)
        {
            Portfolio = portfolio ?? throw new ArgumentNullException(nameof(portfolio));
            InstrumentOwner = owner ?? throw new ArgumentNullException(nameof(owner));
            Instrument = instrument ?? throw new ArgumentNullException(nameof(instrument));
            if(timeSlot<0) throw new ArgumentNullException(nameof(timeSlot));
            TimeSlot = timeSlot;
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
            Price = price;
        }

        public string Portfolio { get; }
        public string InstrumentOwner { get; }
        public string Instrument { get; }
        public int TimeSlot { get; }
        public decimal Price { get; }
    }
}