using MyProjectTravel.Models;
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

        public async Task<string> AddWorkerAsync(Worker WorkerModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(WorkerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Trabajador: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Trabajador: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateWorkerAsync(int id, Worker WorkerModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(WorkerModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"update/{id}", content);

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
                var response = await _httpClient.DeleteAsync($"delete/{id}");

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
