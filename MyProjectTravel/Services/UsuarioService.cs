namespace MyProyectTravel.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<string> GetAllUsuarioAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener Usuario: {ex.Message}", ex);
            }
        }

        public async Task<string> GetUsuarioByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el Usuario: {ex.Message}", ex);
            }
        }

        public async Task<string> AddUsuarioAsync(UsuarioDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Usuario/add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Usuario: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Usuario: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateUsuarioAsync(int id, UsuarioDTO model)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Usuario/update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el Usuario: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteUsuarioAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Usuario/delete/{id}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el Usuario: {ex.Message}", ex);
            }
        }
    }
}