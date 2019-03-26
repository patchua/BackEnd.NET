using Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using DevChallenge.Domain;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Extensions.Logging;
using EFCore.BulkExtensions;

namespace Persistance
{
    public class DbSeeder
    {
        private readonly string format = "dd/MM/yyyy HH:mm:ss";
        private readonly DbService dbService;
        private readonly ILogger<DbSeeder> logger;
        public DbSeeder(DbService service, ILogger<DbSeeder> logger) {
            dbService = service?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void Seed()
        {
            dbService.Database.EnsureCreated();
            if (dbService.InstrumentPrices.AnyAsync().Result)
                return;
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string filePath = path.Substring(6) + @"\data.csv";
            if (File.Exists(filePath))
            {
                var sw = new Stopwatch();
                sw.Start();
                var prices=ReadFromFile(filePath);
                sw.Stop();
                logger.LogInformation($"File processed in {sw.Elapsed.TotalMilliseconds} ms");
                sw=new Stopwatch();
                sw.Start();
                SaveToDb(prices);
                sw.Stop();
                logger.LogInformation($"Data saved to DB in {sw.Elapsed.TotalMilliseconds} ms");
            }
        }

        private void SaveToDb(List<InstrumentPrice> prices)
        {
            //dbService.ChangeTracker.AutoDetectChangesEnabled = false;
            dbService.BulkInsert(prices);
            //dbService.InstrumentPrices.AddRange(prices);
            dbService.Save();
            //dbService.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        private List<InstrumentPrice> ReadFromFile(string filePath)
        {
            List<InstrumentPrice> dataList = new List<InstrumentPrice>();
            var fileStream = new StreamReader(filePath);
            string line;
            fileStream.ReadLine(); // Skip first line
            InstrumentPrice instrumentPrice;
            decimal price;
            DateTime date;
            while ((line = fileStream.ReadLine())!=null)
            {
                var properties = line.Split(',');
                if (Decimal.TryParse(properties[4], out price))
                {
                    if (DateTime.TryParseExact(properties[3],format,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out date))
                    {
                        try
                        {
                            instrumentPrice = new InstrumentPrice(properties[0], properties[1], properties[2], date, TimeSlot.DateTimeToTimeSlot(date), price);
                            dataList.Add(instrumentPrice);
                        }
                        catch (Exception e)
                        {
                            logger.LogInformation(e,"");
                            continue;
                        }
                        
                    }
                    else
                    {
                        logger.LogInformation($"Error parsing Date - {properties[3]}");
                    }
                }
                else
                {
                    logger.LogInformation($"Error parsing Price - {properties[4]}");
                }
                
            }
            return dataList;
        }
    }
}
