namespace MyProyectTravel.Services
{
    public class BoletoService
    {
        private readonly HttpClient _httpClient;

        public BoletoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAllBoletoAsync()
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

        public async Task<string> GetBoletoByIdAsync(int id)
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

        public async Task<string> AddTicketAsync(TicketDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Boleto/add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el boleto: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el boleto: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateBoletoAsync(int id, TicketDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Boleto/update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el boleto: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteBoletoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Boleto/delete/{id}");

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
