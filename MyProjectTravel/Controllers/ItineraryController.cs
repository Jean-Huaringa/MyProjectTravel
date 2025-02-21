using Microsoft.AspNetCore.Mvc;

namespace MyProjectTravel.Controllers
{
    //[Route("Itinerario")]
    public class ItineraryController : Controller
    {
        public async Task<IActionResult> GetAllIteneraryAsync()
        {
            try
            {
                var response = await _accountService.GetAllIteneraryAsync();
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Itenerarys = JsonSerializer.Deserialize<List<Itenerary>>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (Itenerarys == null || Itenerarys.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron Itenerarys.");
                    return View(new List<Itenerary>());
                }

                return View(Itenerarys);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Itenerary>());
            }
        }

        public async Task<IActionResult> GetIteneraryByIdAsync(int id)
        {
            try
            {
                var response = await _accountService.GetIteneraryByIdAsync(id);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Itenerary = JsonSerializer.Deserialize<Itenerary>(response, new JsonSerializerOptions
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

        public async Task<IActionResult> AddIteneraryAsync(IteneraryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _accountService.AddIteneraryAsync(model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var Itenerary = JsonSerializer.Deserialize<Itenerary>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAllIteneraryAsync"); // Redirige a la lista de Itenerarys
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el Itenerary: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> UpdateIteneraryAsync(int id, IteneraryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _accountService.UpdateIteneraryAsync(id, model);
                
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

        public async Task<IActionResult> DeleteIteneraryAsync(int id)
        {

            try
            {
                var response = await _accountService.DeleteIteneraryAsync(id);
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
