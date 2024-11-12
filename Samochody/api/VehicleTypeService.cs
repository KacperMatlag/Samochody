using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Samochody.dtos;
using Samochody.models;

namespace api.Services {
    public class VehicleTypeService {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public VehicleTypeService(HttpClient httpClient, string baseUrl) {
            _httpClient = httpClient;
            _url = baseUrl;
        }

        // Pobierz typ pojazdu po ID
        public async Task<VehicleType> GetVehicleTypeByIdAsync(int vehicleTypeId) {
            try {
                var response = await _httpClient.GetAsync($"{_url}vehicleType/{vehicleTypeId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<VehicleType>(json);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Pobierz wszystkie typy pojazdów
        public async Task<List<VehicleType>> GetAllVehicleTypesAsync() {
            try {
                var response = await _httpClient.GetAsync($"{_url}vehicleType");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<VehicleType>>(json);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Dodaj nowy typ pojazdu
        public async Task<VehicleType> CreateVehicleTypeAsync(VehicleTypeDto dto) {
            try {
                var vehicleType = new VehicleType() {
                    Name = dto.Name,
                };

                var jsonContent = JsonConvert.SerializeObject(vehicleType);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_url}vehicleType", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<VehicleType>(jsonResponse);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Zaktualizuj typ pojazdu
        public async Task<VehicleType> UpdateVehicleTypeAsync(int vehicleTypeId, VehicleTypeDto dto) {
            try {
                var vehicleType = new VehicleType() {
                    Name = dto.Name,
                };

                var jsonContent = JsonConvert.SerializeObject(vehicleType);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_url}vehicleType/{vehicleTypeId}", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<VehicleType>(jsonResponse);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Usuń typ pojazdu
        public async Task<bool> DeleteVehicleTypeAsync(int vehicleTypeId) {
            try {
                var response = await _httpClient.DeleteAsync($"{_url}vehicleType/{vehicleTypeId}");
                response.EnsureSuccessStatusCode();

                return true;
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                return false;
            }
        }
    }
}
