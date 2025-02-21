using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    public class ItineraryController : Controller
    {
        private readonly ItineraryService _itineraryService;

        public ItineraryController(ItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIteneraryAsync()
        {
            try
            {
                var response = await _itineraryService.GetAllIteneraryAsync();
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Itenerarys = JsonSerializer.Deserialize<List<Itinerary>>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Itenerarys == null || Itenerarys.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron Itenerarys.");
                    return View(new List<Itinerary>());
                }

                return View(Itenerarys);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Itinerary>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetIteneraryByIdAsync(int id)
        {
            try
            {
                var response = await _itineraryService.GetIteneraryByIdAsync(id);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Itenerary = JsonSerializer.Deserialize<Itinerary>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Itenerary == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontró el Itenerary especificado.");
                    return View();
                }

                return View(Itenerary);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddIteneraryAsync()
        {
            return View(new Itinerary());
        }

        [HttpPost]
        public async Task<IActionResult> AddIteneraryAsync(ItineraryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var response = await _itineraryService.AddIteneraryAsync(model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Itenerary = JsonSerializer.Deserialize<Itinerary>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAllIteneraryAsync");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el Itenerary: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBusAsync(int id)
        {
            var response = await _itineraryService.GetIteneraryByIdAsync(id);

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
        public async Task<IActionResult> UpdateIteneraryAsync(int id, ItineraryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var response = await _itineraryService.UpdateIteneraryAsync(id, model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllIteneraryAsync");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el Itenerary: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIteneraryAsync(int id)
        {

            try
            {
                var response = await _itineraryService.DeleteIteneraryAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllIteneraryAsync"); // Redirige a la lista de Itenerarys
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el Itenerary: {ex.Message}");
                return View();
            }
        }
    }
}
