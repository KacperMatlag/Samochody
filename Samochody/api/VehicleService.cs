using Newtonsoft.Json;
using Samochody.dtos;
using Samochody.models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Samochody.api {
    public class VehicleService {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public VehicleService(HttpClient httpClient, string baseUrl) {
            _httpClient = httpClient;
            _url = baseUrl;
        }

        // Pobierz pojazd po ID
        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId) {
            try {
                var response = await _httpClient.GetAsync($"{_url}vehicle/{vehicleId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Vehicle>(json);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Pobierz wszystkie pojazdy
        public async Task<List<Vehicle>> GetAllVehiclesAsync() {
            try {
                var response = await _httpClient.GetAsync($"{_url}vehicle");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Vehicle>>(json);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Dodaj nowy pojazd
        public async Task<Vehicle> CreateVehicleAsync(VehicleDto dto) {
            try {
                var vehicle = new Vehicle() {
                    Model = dto.Model,
                    Year = dto.Year,
                    Color = dto.Color,
                    GearboxID = dto.GearboxID,
                    Mileage = dto.Mileage,
                    EngineID = dto.EngineID,
                    SeatingCapacity = dto.SeatingCapacity,
                    BodyType = dto.BodyType,
                    VIN = dto.VIN,
                    Price = dto.Price,
                    VehicleTypeID = dto.VehicleTypeID
                };

                var jsonContent = JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_url}vehicle", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Vehicle>(jsonResponse);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Aktualizuj pojazd
        public async Task<Vehicle> UpdateVehicleAsync(int vehicleId, VehicleDto dto) {
            try {
                var vehicle = new Vehicle() {
                    Model = dto.Model,
                    Year = dto.Year,
                    Color = dto.Color,
                    GearboxID = dto.GearboxID,
                    Mileage = dto.Mileage,
                    EngineID = dto.EngineID,
                    SeatingCapacity = dto.SeatingCapacity,
                    BodyType = dto.BodyType,
                    VIN = dto.VIN,
                    Price = dto.Price,
                    VehicleTypeID = dto.VehicleTypeID
                };

                var jsonContent = JsonConvert.SerializeObject(vehicle);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_url}vehicle/{vehicleId}", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Vehicle>(jsonResponse);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Usuń pojazd
        public async Task<bool> DeleteVehicleAsync(int vehicleId) {
            try {
                var response = await _httpClient.DeleteAsync($"{_url}vehicle/{vehicleId}");
                response.EnsureSuccessStatusCode();

                return true;
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                return false;
            }
        }
    }
}
