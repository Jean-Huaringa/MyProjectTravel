using MyProjectTravel.Models.DTO;
using Newtonsoft.Json;

namespace MyProyectTravel.Services
{
    public class ItinerarioService
    {
        private readonly HttpClient _httpClient;

        public ItinerarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

 
        public async Task<string> GetAllItinerarioAsync()
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

        public async Task<string> GetItinerarioByIdAsync(int id)
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

        public async Task<string> AddItinerarioAsync(ItinerarioDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Itinerario/add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Itinerario: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Itinerario: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateItinerarioAsync(int id, ItinerarioDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Itinerario/update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el Itinerario: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteItinerarioAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Itinerario/delete/{id}");

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
