namespace MyProyectTravel.Services
{
    public class BusService 
    {
        private readonly HttpClient _httpClient;

        public BusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAllBusAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener Bus: {ex.Message}", ex);
            }
        }

        public async Task<string> GetBusByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el Bus: {ex.Message}", ex);
            }
        }

        public async Task<string> AddBusAsync(BusDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Bus/add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Bus: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Bus: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateBusAsync(int id, BusDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Bus/update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el Bus: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteBusAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Bus/delete/{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el Bus: {ex.Message}", ex);
            }
        }
    }
}
