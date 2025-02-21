using MyProjectTravel.Models.DTO;
using System.Text;
using System.Text.Json;

namespace MyProyectTravel.Services.Public
{
    public class PasajesService
    {
        private readonly HttpClient _httpClient;
    
        public PasajesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    
        public async Task<string> GetAllEstacionesAsync()
        {
            var response = await _httpClient.GetAsync("all");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    
        public async Task<string> SearchForPassageAsync(int? idOrigen, int? idDestino, DateTime? fechaInicio)
        {
            var query = $"filter-for-passage?idOrigen={idOrigen}&idDestino={idDestino}&fechaInicio={fechaInicio?.ToString("yyyy-MM-dd")}";
    
            var response = await _httpClient.PostAsync(query, null);

            response.EnsureSuccessStatusCode();
                
            return await response.Content.ReadAsStringAsync();
        }
    
        public async Task<string> SearchForInformationPassageAsync(int id)
        {
            var response = await _httpClient.GetAsync($"search-for-information-passage/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    
        public async Task<string> BuyTicketAsync(TicketDTO model)
        {
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
    
            var response = await _httpClient.PostAsync("buy-ticket/", content);
            response.EnsureSuccessStatusCode();
    
            return await response.Content.ReadAsStringAsync();
        }
    }
}
