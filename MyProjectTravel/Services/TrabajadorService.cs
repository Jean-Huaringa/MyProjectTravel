namespace MyProyectTravel.Services
{
    public class TrabajadorService
    {
        private readonly HttpClient _httpClient;

        public TrabajadorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<string> GetAllTrabajadorAsync()
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

        public async Task<string> GetTrabajadorByIdAsync(int id)
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

        public async Task<string> AddTrabajadorAsync(TrabajadorDTO model)
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

        public async Task<string> UpdateTrabajadorAsync(int id, TrabajadorDTO model)
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

        public async Task<string> DeleteTrabajadorAsync(int id)
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
