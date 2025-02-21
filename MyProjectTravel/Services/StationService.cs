using MyProjectTravel.Models;
using System.Text;
using System.Text.Json;

namespace MyProyectTravel.Services
{
    public class StationService
    {

        private readonly HttpClient _httpClient;

        public StationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<string> GetAllStationAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener Estacion: {ex.Message}", ex);
            }
        }

        public async Task<string> GetStationByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el Estacion: {ex.Message}", ex);
            }
        }

        public async Task<string> AddStationAsync(Station StationModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(StationModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Estacion: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Estacion: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateStationAsync(int id, Station StationModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(StationModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el Estacion: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteStationAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"delete/{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el Estacion: {ex.Message}", ex);
            }
        }
    }
}
