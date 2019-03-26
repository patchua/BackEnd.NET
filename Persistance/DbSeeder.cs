using Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DevChallenge.Domain;

namespace Persistance
{
    public class DbSeeder
    {
        private readonly DbService dbService;
        public DbSeeder(DbService service) {
            dbService = service?? throw new ArgumentNullException(nameof(service));
        }
        public void Seed()
        {
            dbService.Database.EnsureCreated();
            if (dbService.InstrumentPrices.AnyAsync().Result)
                return;
            //            string filePath = @"./src/SC.DevChallenge.Api/Input/data.csv";
            var path = System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string filePath = path.Substring(6) + @"\data.csv";
            if (File.Exists(filePath))
            {
                SeedFromFile(filePath);
            }            
        }

        private void SeedFromFile(string filePath)
        {
            List<InstrumentPrice> dataList = new List<InstrumentPrice>();
            var fileStream = new StreamReader(filePath);
            string line;
            fileStream.ReadLine(); // Skip first line
            InstrumentPrice obj;
            decimal price;
            DateTime date;
            while ((line = fileStream.ReadLine())!=null)
            {
                var props = line.Split(',');
                if (Decimal.TryParse(props[4], out price))
                {
                    if (DateTime.TryParseExact(props[3],"DD/MM/yyyy HH:MM:ss",new IFormatProvider  out date))
                    {
                        obj = new InstrumentPrice(props[0], props[1], props[2], date,TimeSlot.DateTimeToTimeSlot(date), price);
                        dataList.Add(obj);
                    }                    
                }
                
            }
        }
    }
}
