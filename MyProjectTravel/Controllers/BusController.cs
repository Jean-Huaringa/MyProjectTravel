using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    [Route("Bus")]
    public class BusController : Controller
    {
        private readonly BusService _busService;

        public BusController(BusService busService)
        {
            _busService = busService;
        }

        [HttpGet("GetAll")]
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

                return View("GetAll", Buss);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Bus>());
            }
        }

        [HttpGet("GetBusById")]
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

                return View("GetBusById", Bus);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }
        
        [HttpGet("AddBus")]
        public async Task<IActionResult> AddBusAsync()
        {
            return View("AddBus", new Bus());
        }

        [HttpPost("AddBus")]
        public async Task<IActionResult> AddBusAsync(Bus BusModel)
        {
            if (!ModelState.IsValid)
            {
                return View(BusModel); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _busService.AddBusAsync(BusModel);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Bus = JsonSerializer.Deserialize<Bus>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAll"); // Redirige a la lista de Buss
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el Bus: {ex.Message}");
                return View(BusModel);
            }
        }

        [HttpGet("UpdateBus")]
        public async Task<IActionResult> UpdateBusAsync(int id)
        {
            var response = await _busService.GetBusByIdAsync(id);

            if (response == null)
            {
                return NotFound(new { message = "No se encontró el Bus especificado." });
            }

            try
            {
                var busEntity = JsonSerializer.Deserialize<Bus>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (busEntity == null)
                {
                    return NotFound(new { message = "No se pudo procesar la respuesta del servidor." });
                }

                return View("UpdateBus", busEntity);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { message = $"Error de deserialización: {ex.Message}" });
            }
        }

        [HttpPost("UpdateBus")]
        public async Task<IActionResult> UpdateBusAsync([FromForm] Bus BusModel)
        {
            try
            {
                var response = await _busService.UpdateBusAsync(BusModel.idBus, BusModel);

                if (response == null)
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el Bus.");
                    return View(BusModel);
                }

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el Bus: {ex.Message}");
                return View(BusModel);
            }
        }

        [HttpPost("DeleteBus")]
        public async Task<IActionResult> DeleteBusAsync(int id)
        {

            try
            {
                var response = await _busService.DeleteBusAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el Bus: {ex.Message}");
                return View();
            }
        }
    }
}
