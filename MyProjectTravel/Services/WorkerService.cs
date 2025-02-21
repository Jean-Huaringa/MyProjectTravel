using MyProjectTravel.Models.DTO;
using System.Text;
using System.Text.Json;

namespace MyProyectTravel.Services
{
    public class WorkerService
    {
        private readonly HttpClient _httpClient;

        public WorkerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAllWorkerAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener Trabajador: {ex.Message}", ex);
            }
        }

        public async Task<string> GetWorkerByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el Trabajador: {ex.Message}", ex);
            }
        }

        public async Task<string> AddWorkerAsync(WorkerDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Trabajador/add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Trabajador: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Trabajador: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateWorkerAsync(int id, WorkerDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Trabajador/update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el Trabajador: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteWorkerAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Trabajador/delete/{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el Trabajador: {ex.Message}", ex);
            }
        }
    }
}
