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
using Microsoft.Extensions.Configuration;

namespace Persistance
{
    public class DbSeeder
    {
        private readonly string _format;
        private readonly DbService _dbService;
        private readonly ILogger<DbSeeder> _logger;
        private readonly string _file;
        public DbSeeder(DbService service, ILogger<DbSeeder> logger, IConfiguration config) {
            _dbService = service?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _format = config.GetValue<string>("DateTimeFormat");
            _file = config.GetValue<string>("DbSeedFile");
        }
        public void Seed()
        {
            _dbService.Database.EnsureCreated();
            if (_dbService.InstrumentPrices.AnyAsync().Result)
                return;
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            string filePath = Path.Combine(path,_file);
            if (File.Exists(filePath))
            {
                var sw = new Stopwatch();
                sw.Start();
                var prices=ReadFromFile(filePath);
                sw.Stop();
                _logger.LogInformation($"File processed in {sw.Elapsed.TotalMilliseconds} ms");
                sw.Restart();
                SaveToDb(prices);
                sw.Stop();
                _logger.LogInformation($"Data saved to DB in {sw.Elapsed.TotalMilliseconds} ms");
            }
        }

        private void SaveToDb(List<InstrumentPrice> prices)
        {
            _dbService.BulkInsert(prices);
            _dbService.Save();
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
                    if (DateTime.TryParseExact(properties[3],_format,
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
                            _logger.LogInformation(e,"");
                            continue;
                        }
                        
                    }
                    else
                    {
                        _logger.LogInformation($"Error parsing Date - {properties[3]}");
                    }
                }
                else
                {
                    _logger.LogInformation($"Error parsing Price - {properties[4]}");
                }
                
            }
            return dataList;
        }
    }
}
