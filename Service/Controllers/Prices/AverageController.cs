using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.InstrumentPrices.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Service.Controllers.Prices
{
    [Route("api/prices/[controller]")]
    [ApiController]
    public class AverageController : ControllerBase
    {
        private readonly string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        private readonly IGetAveragePriceQuery _getAveragePriceQuery;
        private readonly ILogger<AverageController> _logger;
        public AverageController(IGetAveragePriceQuery getAveragePriceQuery, ILogger<AverageController> logger)
        {
            _getAveragePriceQuery = getAveragePriceQuery ?? throw new ArgumentNullException(nameof(getAveragePriceQuery));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Average
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string portfolio, [FromQuery]string instrument,
            [FromQuery] string owner, [FromQuery]string dateAsString)
        {
            if (portfolio == null)
                return BadRequest();
            if (owner == null)
                return BadRequest();
            if (dateAsString == null)
                return BadRequest();
            if (instrument == null)
                return BadRequest();
            DateTime date;
            if (!DateTime.TryParseExact(dateAsString, dateTimeFormat, null, DateTimeStyles.None, out date))
                return BadRequest($"Invalid date format - {dateAsString}");
            try
            {
                var average = await _getAveragePriceQuery.ExecuteAsync(date,instrument,owner,portfolio);
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
