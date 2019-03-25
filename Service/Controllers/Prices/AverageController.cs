using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.InstrumentPrices.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Service.Controllers.Prices
{
    [Route("api/[controller]")]
    [ApiController]
    public class AverageController : ControllerBase
    {
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
            [FromQuery] string owner, [FromQuery]DateTime date)
        {
            if (portfolio == null)
                return BadRequest();
            if (owner == null)
                return BadRequest();
            if (date == null)
                return BadRequest();
            if (instrument == null)
                return BadRequest();

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
