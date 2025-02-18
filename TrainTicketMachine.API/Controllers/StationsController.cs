using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainTicketMachine.Application.Contracts;

namespace TrainTicketMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var fındStatıonsbySerachQuery = await _stationService.SearchStationsAsync(query);

            return Ok(fındStatıonsbySerachQuery);
        }
    }
}
