using Newtonsoft.Json;
using Samochody.dtos;
using Samochody.models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Samochody.api {
    public class EngineService {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public EngineService(HttpClient httpClient, string baseUrl) {
            _httpClient = httpClient;
            _url = baseUrl;
        }

        // Pobierz silnik po ID
        public async Task<Engine> GetEngineByIdAsync(int engineId) {
            try {
                var response = await _httpClient.GetAsync($"{_url}engine/{engineId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Engine>(json);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Dodaj nowy silnik
        public async Task<Engine> CreateEngineAsync(EngineDto engine) {
            try {
                var jsonContent = JsonConvert.SerializeObject(engine);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_url}engine", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Engine>(jsonResponse);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Zaktualizuj istniejący silnik
        public async Task<Engine> UpdateEngineAsync(int engineId, EngineDto engine) {
            try {
                var jsonContent = JsonConvert.SerializeObject(engine);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_url}engine/{engineId}", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Engine>(jsonResponse);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }

        // Usuń silnik
        public async Task<bool> DeleteEngineAsync(int engineId) {
            try {
                var response = await _httpClient.DeleteAsync($"{_url}engine/{engineId}");
                response.EnsureSuccessStatusCode();

                return true;
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                return false;
            }
        }

        // Pobierz wszystkie silniki
        public async Task<List<Engine>> GetAllEnginesAsync() {
            try {
                var response = await _httpClient.GetAsync($"{_url}engine");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Engine>>(json);
            } catch(Exception ex) {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw;
            }
        }
    }
}
