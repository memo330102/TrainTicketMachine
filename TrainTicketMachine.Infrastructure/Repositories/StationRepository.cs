using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly IStationDataSource _dataSource;
        private static List<StationDataSourceResponse> _cachedStations = new List<StationDataSourceResponse>();
        public StationRepository(IStationDataSource remoteStationProvider)
        {
            _dataSource = remoteStationProvider;
        }

        public async Task<List<StationDataSourceResponse>> SearchStationsAsync(string query)
        {
            return await _dataSource.LoadStationsAsync();
        }
    }
}
