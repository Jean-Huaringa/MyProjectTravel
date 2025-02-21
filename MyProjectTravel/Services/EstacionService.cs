namespace MyProyectTravel.Services
{
    public class EstacionService
    {

        private readonly HttpClient _httpClient;

        public EstacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<string> GetAllEstacionAsync()
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

        public async Task<string> GetEstacionByIdAsync(int id)
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

        public async Task<string> AddEstacionAsync(EstacionDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Estacion/add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Estacion: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Estacion: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateEstacionAsync(int id, EstacionDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Estacion/update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el Estacion: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteEstacionAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Estacion/delete/{id}");

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
