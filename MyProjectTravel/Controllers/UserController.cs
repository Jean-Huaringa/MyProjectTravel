using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    [Route("User")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
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

                return View("GetAll", Users);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<User>());
            }
        }

        [HttpGet("GetUserById")]
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

                return View("GetUserById", User);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        [HttpGet("AddUser")]
        public async Task<IActionResult> AddUserAsync()
        {
            return View("AddUser", new User());
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserAsync(User UserModel)
        {
            if (!ModelState.IsValid)
            {
                return View(UserModel); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _userService.AddUserAsync(UserModel);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var User = JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el User: {ex.Message}");
                return View(UserModel);
            }
        }

        [HttpGet("UpdateUser")]
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

            return RedirectToAction("GetAll");
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(int id, User UserModel)
        {
            if (!ModelState.IsValid)
            {
                return View(UserModel); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _userService.UpdateUserAsync(id, UserModel);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el User: {ex.Message}");
                return View(UserModel);
            }
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {

            try
            {
                var response = await _userService.DeleteUserAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el User: {ex.Message}");
                return View();
            }
        }
    }
}
