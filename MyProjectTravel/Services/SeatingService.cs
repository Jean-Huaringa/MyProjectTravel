using MyProjectTravel.Models.DTO;
using System.Text;
using System.Text.Json;

namespace MyProyectTravel.Services
{
    public class SeatingService
    {
        private readonly HttpClient _httpClient;

        public SeatingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAllSeatingAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener Seating: {ex.Message}", ex);
            }
        }

        public async Task<string> AddSeatinngByBusAsync(int idBus)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{idBus}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el Seating: {ex.Message}", ex);
            }
        }

        public async Task<string> TakeSeatAsync(int idBus, int row, int column)
        {
            try
            {
                var query = $"take-seat?idBus={idBus}&row={row}&column={column}";
                
                var response = await _httpClient.PostAsync(query, null);
            
                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Seating: {ex.Message}", ex);
            }
        }

    }
}
