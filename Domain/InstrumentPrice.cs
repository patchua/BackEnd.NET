using Domain;
using System;

namespace DevChallenge.Domain
{
    public class InstrumentPrice: Entity
    {
        public InstrumentPrice(string portfolio, string owner, string instrument,DateTime date, int timeSlot, decimal price)
        {
            Portfolio = portfolio ?? throw new ArgumentNullException(nameof(portfolio));
            InstrumentOwner = owner ?? throw new ArgumentNullException(nameof(owner));
            Instrument = instrument ?? throw new ArgumentNullException(nameof(instrument));
            if(timeSlot<0) throw new ArgumentNullException(nameof(timeSlot));
            TimeSlot = timeSlot;
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
            Price = price;
            Date = date;
        }
        private InstrumentPrice()
        { }   
        public string Portfolio { get; private set; }
        public string InstrumentOwner { get; private set; }
        public string Instrument { get; private set; }
        public int TimeSlot { get; private set; }
        public decimal Price { get; private set; }
        public DateTime Date { get; private set; }
    }
}