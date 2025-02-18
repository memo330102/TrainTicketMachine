﻿using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Providers
{
    public class JsonStationProvider : IStationDataSource
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _dataUrl;


        public JsonStationProvider(IConfiguration configuration)
        {
            _dataUrl = configuration["StationData:Url"] ?? "";
        }

        public async Task<List<RemoteStationResponse>> LoadStationsAsync()
        {

            var response = await _httpClient.GetStringAsync(_dataUrl);
            var stationData = JsonSerializer.Deserialize<List<RemoteStationResponse>>(response);

            return stationData;
        }
    }
}