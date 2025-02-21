using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    [Route("Worker")]
    [Authorize(Roles = "admin")]
    public class WorkerController : Controller
    {
        private readonly WorkerService _workerService;

        public WorkerController(WorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet("GetAll")]
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

                return View("GetAll", Workers);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Worker>());
            }
        }

        [HttpGet("GetWorker")]
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

                return View("GetWorker", Worker);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        [HttpGet("AddWorker")]
        public async Task<IActionResult> AddWorkerAsync()
        {
            return View("AddWorker", new Worker());
        }

        [HttpPost("AddWorker")]
        public async Task<IActionResult> AddWorkerAsync(Worker WorkerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(WorkerModel); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _workerService.AddWorkerAsync(WorkerModel);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Worker = JsonSerializer.Deserialize<Worker>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el Worker: {ex.Message}");
                return View(WorkerModel);
            }
        }

        [HttpGet("UpdateWorker")]
        public async Task<IActionResult> UpdateWorkerAsync(int id)
        {
            var response = await _workerService.GetWorkerByIdAsync(id);

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

            return View("UpdateWorker", busEntity);
        }

        [HttpPost("UpdateWorker")]
        public async Task<IActionResult> UpdateWorkerAsync(int id, Worker WorkerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(WorkerModel); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _workerService.UpdateWorkerAsync(id, WorkerModel);

                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el Worker: {ex.Message}");
                return View(WorkerModel);
            }
        }

        [HttpPost("DeleteWorker")]
        public async Task<IActionResult> DeleteWorkerAsync(int id)
        {

            try
            {
                var response = await _workerService.DeleteWorkerAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el Worker: {ex.Message}");
                return View();
            }
        }
    }
}
