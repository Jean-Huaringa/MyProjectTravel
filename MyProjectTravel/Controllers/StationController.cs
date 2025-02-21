using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    [Route("Station")]
    public class StationController : Controller
    {
        private readonly StationService _stationService;

        public StationController(StationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStationAsync()
        {
            try
            {
                var response = await _stationService.GetAllStationAsync();
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

                return View("GetAll", Stations);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Station>());
            }
        }

        [HttpGet("GetStationById")]
        public async Task<IActionResult> GetStationByIdAsync(int id)
        {
            try
            {
                var response = await _stationService.GetStationByIdAsync(id);
                
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

                return View("GetStationById", Station);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        [HttpGet("AddStation")]
        public async Task<IActionResult> AddStationAsync()
        {
            return View("AddStation",new Station());
        }

        [HttpPost("AddStation")]
        public async Task<IActionResult> AddStationAsync(Station StationModel)
        {
            try
            {
                var response = await _stationService.AddStationAsync(StationModel);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Station = JsonSerializer.Deserialize<Station>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el Station: {ex.Message}");
                return View(StationModel);
            }
        }

        [HttpGet("UpdateStation")]
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

        [HttpPost("UpdateStation")]
        public async Task<IActionResult> UpdateStationAsync(int id, Station StationModel)
        {
            if (!ModelState.IsValid)
            {
                return View(StationModel); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _stationService.UpdateStationAsync(id, StationModel);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el Station: {ex.Message}");
                return View(StationModel);
            }
        }

        [HttpPost("DeleteStation")]
        public async Task<IActionResult> DeleteStationAsync(int id)
        {

            try
            {
                var response = await _stationService.DeleteStationAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el Station: {ex.Message}");
                return View();
            }
        }
    }
}
