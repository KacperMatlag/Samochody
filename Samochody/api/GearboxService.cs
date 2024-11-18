using Newtonsoft.Json;
using Samochody.dtos;
using Samochody.models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Samochody.api {
    public class GearboxService {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public GearboxService(HttpClient httpClient, string baseUrl) {
            _httpClient = httpClient;
            _url = baseUrl;
        }

        // Pobierz skrzynię biegów po ID
        public async Task<Gearbox> GetGearboxByIdAsync(int gearboxId) {
            try {
                var response = await _httpClient.GetAsync($"{_url}gearbox/{gearboxId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Gearbox>(json);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Dodaj nową skrzynię biegów
        public async Task<Gearbox> CreateGearboxAsync(GearboxDto gearbox) {
            try {
                var jsonContent = JsonConvert.SerializeObject(gearbox);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_url}gearbox", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Gearbox>(jsonResponse);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Zaktualizuj istniejącą skrzynię biegów
        public async Task<Gearbox> UpdateGearboxAsync(int gearboxId, GearboxDto gearbox) {
            try {
                var jsonContent = JsonConvert.SerializeObject(gearbox);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_url}gearbox/{gearboxId}", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Gearbox>(jsonResponse);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Usuń skrzynię biegów
        public async Task<bool> DeleteGearboxAsync(int gearboxId) {
            try {
                var response = await _httpClient.DeleteAsync($"{_url}gearbox/{gearboxId}");
                response.EnsureSuccessStatusCode();

                return true;
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                return false;
            }
        }

        // Pobierz wszystkie skrzynie biegów
        public async Task<List<Gearbox>> GetAllGearboxesAsync() {
            try {
                var response = await _httpClient.GetAsync($"{_url}gearbox");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Gearbox>>(json);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }
    }
}
