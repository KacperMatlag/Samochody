using api.Services;
using Samochody.models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Samochody.api {
    public class ApiService {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://10.0.2.2:5204/api/";
        public ApiService() {
            _httpClient = new HttpClient {
                Timeout = TimeSpan.FromSeconds(30)
            };
            Engine = new EngineService(_httpClient, BaseUrl);
            VehicleService = new VehicleService(_httpClient, BaseUrl);
            VehicleTypeService=new VehicleTypeService(_httpClient, BaseUrl);
        }
        public EngineService Engine { get; }
        public VehicleService VehicleService { get; }
        public VehicleTypeService VehicleTypeService { get; }
    }
}
