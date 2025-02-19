using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly IEnumerable<IStationDataSource> _dataSources;

        public StationRepository(IEnumerable<IStationDataSource> dataSources)
        {
            _dataSources = dataSources;
        }

        public async Task<HashSet<StationDataSourceResponse>> SearchStationsAsync()
        {
            var dataTasks = _dataSources.Select(ds => ds.LoadStationsAsync());
            var results = await Task.WhenAll(dataTasks);

            var combinedStations = results
                                   .SelectMany(stationList => stationList)
                                   .GroupBy(s => s.stationCode, StringComparer.OrdinalIgnoreCase)
                                   .Select(g => g.First())
                                   .ToHashSet();


            return combinedStations;
        }
    }
}
