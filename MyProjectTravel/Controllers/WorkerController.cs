using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    [Authorize(Roles = "admin")]
    public class WorkerController : Controller
    {
        private readonly WorkerService _workerService;

        public WorkerController(WorkerService workerService)
        {
            _workerService = workerService;
        }

        public async Task<IActionResult> GetAllWorkerAsync()
        {
            try
            {
                var response = await _workerService.GetAllWorkerAsync();
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Workers = JsonSerializer.Deserialize<List<Worker>>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Workers == null || Workers.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron Workers.");
                    return View(new List<Worker>());
                }

                return View(Workers);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Worker>());
            }
        }

        public async Task<IActionResult> GetWorkerByIdAsync(int id)
        {
            try
            {
                var response = await _workerService.GetWorkerByIdAsync(id);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Worker = JsonSerializer.Deserialize<Worker>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Worker == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontró el Worker especificado.");
                    return View();
                }

                return View(Worker);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        public async Task<IActionResult> AddWorkerAsync(WorkerDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _workerService.AddWorkerAsync(model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Worker = JsonSerializer.Deserialize<Worker>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAllWorkerAsync"); // Redirige a la lista de Workers
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el Worker: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> UpdateWorkerAsync(int id, WorkerDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _workerService.UpdateWorkerAsync(id, model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllWorkerAsync");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el Worker: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteWorkerAsync(int id)
        {

            try
            {
                var response = await _workerService.DeleteWorkerAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllWorkerAsync"); // Redirige a la lista de Workers
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el Worker: {ex.Message}");
                return View();
            }
        }
    }
}
