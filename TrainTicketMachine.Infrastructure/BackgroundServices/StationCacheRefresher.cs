using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainTicketMachine.Infrastructure.Contracts;

namespace TrainTicketMachine.Infrastructure.BackgroundServices
{
    public class StationCacheRefresher : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _refreshInterval = TimeSpan.FromMinutes(30);
        private readonly string _stationListCacheKey;

        public StationCacheRefresher(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _stationListCacheKey = configuration["StationData:StationListCacheKey"] ?? "";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IStationRepository>();
                    var stations = await repository.SearchStationsAsync();

                    var _cache = scope.ServiceProvider.GetRequiredService<IStationCacheService>();
                    await _cache.SetCacheValueAsync(_stationListCacheKey, stations, _refreshInterval);
                }
                await Task.Delay(_refreshInterval, stoppingToken);
            }
        }
    }
}
