using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainTicketMachine.Domain.Interfaces.Application;

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
            var stations = await _stationService.SearchStationsAsync(query);
            var nextChars = await _stationService.GetNextCharactersAsync(query);
            return Ok(new { Stations = stations, NextCharacters = nextChars });
        }
    }
}
