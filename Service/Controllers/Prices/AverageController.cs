using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.InstrumentPrices.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Service.Controllers.Prices
{
    [Route("api/prices/[controller]")]
    [ApiController]
    public class AverageController : ControllerBase
    {
        private readonly string _dateTimeFormat ;
        private readonly IGetAveragePriceQuery _getAveragePriceQuery;
        private readonly ILogger<AverageController> _logger;
        public AverageController(IGetAveragePriceQuery getAveragePriceQuery, ILogger<AverageController> logger, IConfiguration config)
        {
            _getAveragePriceQuery = getAveragePriceQuery ?? throw new ArgumentNullException(nameof(getAveragePriceQuery));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dateTimeFormat = config.GetValue<string>("DateTimeFormat");
        }

        // GET: api/Average
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string portfolio, [FromQuery]string instrument,
            [FromQuery] string owner, [FromQuery]string date)
        {
            if (portfolio == null)
                return BadRequest();
            if (owner == null)
                return BadRequest();
            if (date == null)
                return BadRequest();
            if (instrument == null)
                return BadRequest();
            DateTime dateTimeParsed;
            if (!DateTime.TryParseExact(date, _dateTimeFormat, null, DateTimeStyles.None, out dateTimeParsed))
                return BadRequest($"Invalid date format - {date}");
            try
            {
                var average = await _getAveragePriceQuery.ExecuteAsync(dateTimeParsed,instrument,owner,portfolio);
                if (average == null)
                    return NotFound();
                return Ok(average);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Error in AveragePrice request for Portfolio - {0}, Owner - {1}, Instrument - {2}, Date - {3}",
                    portfolio,owner,instrument,date);
                throw;
            }
        }
    }
}
