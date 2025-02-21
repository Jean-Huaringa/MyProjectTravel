using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    public class StatinoController : Controller
    {
        private readonly StationService _stationService;

        public StatinoController(StationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStationAsync()
        {
            try
            {
                var response = await _statinoService.GetAllStationAsync();
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Stations = JsonSerializer.Deserialize<List<Station>>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Stations == null || Stations.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron Stations.");
                    return View(new List<Station>());
                }

                return View(Stations);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Station>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStationByIdAsync(int id)
        {
            try
            {
                var response = await _statinoService.GetStationByIdAsync(id);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Station = JsonSerializer.Deserialize<Station>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Station == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontró el Station especificado.");
                    return View();
                }

                return View(Station);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddStationAsync()
        {
            return View(new Station());
        }

        [HttpPost]
        public async Task<IActionResult> AddStationAsync(StationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _statinoService.AddStationAsync(model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Station = JsonSerializer.Deserialize<Station>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAllStationAsync"); // Redirige a la lista de Stations
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el Station: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStationAsync(int id)
        {
            var response = await _stationService.GetStationByIdAsync(id);

            if (response == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            var itineraryEntity = JsonSerializer.Deserialize<Bus>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (itineraryEntity == null)
            {
                ModelState.AddModelError(string.Empty, "No se encontró el Bus especificado.");
                return View();
            }

            return View(itineraryEntity);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStationAsync(int id, StationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _statinoService.UpdateStationAsync(id, model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllStationAsync");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el Station: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStationAsync(int id)
        {

            try
            {
                var response = await _statinoService.DeleteStationAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllStationAsync"); // Redirige a la lista de Stations
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el Station: {ex.Message}");
                return View();
            }
        }
    }
}
