﻿using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using System.Text;
using System.Text.Json;

namespace MyProyectTravel.Services
{
    public class ItineraryService
    {
        private readonly HttpClient _httpClient;

        public ItineraryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAllIteneraryAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener Itinerario: {ex.Message}", ex);
            }
        }

        public async Task<string> GetIteneraryByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el Itinerario: {ex.Message}", ex);
            }
        }

        public async Task<string> AddIteneraryAsync(Itinerary ItineraryModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(ItineraryModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Itinerario: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Itinerario: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateIteneraryAsync(int id, Itinerary ItineraryModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(ItineraryModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el Itinerario: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteIteneraryAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"delete/{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el Itinerario: {ex.Message}", ex);
            }
        }
    }
}
