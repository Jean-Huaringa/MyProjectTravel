using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            try
            {
                var response = await _userService.GetAllUserAsync();
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Users = JsonSerializer.Deserialize<List<User>>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Users == null || Users.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron Users.");
                    return View(new List<User>());
                }

                return View(Users);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<User>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            try
            {
                var response = await _userService.GetUserByIdAsync(id);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var User = JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (User == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontró el User especificado.");
                    return View();
                }

                return View(User);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddUserAsync()
        {
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _userService.AddUserAsync(model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var User = JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAllUserAsync"); // Redirige a la lista de Users
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el User: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserAsync(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);

            if (response == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            var busEntity = JsonSerializer.Deserialize<Bus>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (busEntity == null)
            {
                ModelState.AddModelError(string.Empty, "No se encontró el Bus especificado.");
                return View();
            }

            return View(busEntity);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync(int id, UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _userService.UpdateUserAsync(id, model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllUserAsync");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el User: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {

            try
            {
                var response = await _userService.DeleteUserAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllUserAsync"); // Redirige a la lista de Users
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el User: {ex.Message}");
                return View();
            }
        }
    }
}
