using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly IEnumerable<IStationDataSource> _dataSources;
        private readonly IStationHelper _helper;
        public StationRepository(IEnumerable<IStationDataSource> dataSources, IStationHelper helper)
        {
            _dataSources = dataSources;
            _helper = helper;
        }

        public async Task<HashSet<StationDataSourceResponse>> SearchStationsAsync()
        {
            var dataTasks = _dataSources.Select(ds => ds.LoadStationsAsync());
            var results = await Task.WhenAll(dataTasks);

            var combinedStations = _helper.CombineStations(results);

            return combinedStations;
        }
    }
}
