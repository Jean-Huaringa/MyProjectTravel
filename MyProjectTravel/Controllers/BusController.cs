using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    public class BusController : Controller
    {
        private readonly BusService _busService;

        public BusController(BusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBusAsync()
        {
            try
            {
                var response = await _busService.GetAllBusAsync();
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Buss = JsonSerializer.Deserialize<List<Bus>>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Buss == null || Buss.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron Buss.");
                    return View(new List<Bus>());
                }

                return View(Buss);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Bus>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBusByIdAsync(int id)
        {
            try
            {
                var response = await _busService.GetBusByIdAsync(id);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Bus = JsonSerializer.Deserialize<Bus>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Bus == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontró el Bus especificado.");
                    return View();
                }

                return View(Bus);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddBusAsync()
        {
            return View(new Bus());
        }

        [HttpPost]
        public async Task<IActionResult> AddBusAsync(BusDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _busService.AddBusAsync(model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Bus = JsonSerializer.Deserialize<Bus>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAllBusAsync"); // Redirige a la lista de Buss
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el Bus: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBusAsync(int id)
        {
            var response = await _busService.GetBusByIdAsync(id);

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
        public async Task<IActionResult> UpdateBusAsync(int id, BusDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _busService.UpdateBusAsync(id, model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllBusAsync");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el Bus: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBusAsync(int id)
        {

            try
            {
                var response = await _busService.DeleteBusAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllBusAsync"); // Redirige a la lista de Buss
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el Bus: {ex.Message}");
                return View();
            }
        }
    }
}
