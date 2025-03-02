﻿using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using System.Text;
using System.Text.Json;

namespace MyProyectTravel.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    
        public async Task<string> GetAllUserAsync()
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

        public async Task<string> GetUserByIdAsync(int id)
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

        public async Task<string> AddUserAsync(User UserModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(UserModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("add", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

                throw new Exception($"Error al agregar el Usuario: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el Usuario: {ex.Message}", ex);
            }
        }

        public async Task<string> UpdateUserAsync(int id, User UserModel)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(UserModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"update/{id}", content);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el Usuario: {ex.Message}", ex);
            }
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"delete/{id}");

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