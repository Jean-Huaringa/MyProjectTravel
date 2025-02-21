using MyProjectTravel.Models;
using System.Text;
using System.Text.Json;

namespace MyProyectTravel.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAllTicketAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener boletos: {ex.Message}", ex);
            }
        }

        public async Task<string> GetTicketByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el boleto: {ex.Message}", ex);
            }
        }

        public async Task<string> AddTicketAsync(Ticket TicketModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(TicketModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el boleto: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateTicketAsync(int id, Ticket TicketModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(TicketModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el boleto: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteTicketAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"delete/{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el boleto: {ex.Message}", ex);
            }
        }
    }
}
