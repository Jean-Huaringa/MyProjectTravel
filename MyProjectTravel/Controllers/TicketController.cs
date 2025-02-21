using Microsoft.AspNetCore.Mvc;

namespace MyProjectTravel.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IActionResult> GetAllTicketAsync()
        {
            try
            {
                var response = await _accountService.GetAllTicketAsync();
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var tickets = JsonSerializer.Deserialize<List<Ticket>>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (tickets == null || tickets.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron tickets.");
                    return View(new List<Ticket>());
                }

                return View(tickets);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Ticket>());
            }
        }

        public async Task<IActionResult> GetTicketByIdAsync(int id)
        {
            try
            {
                var response = await _accountService.GetTicketByIdAsync(id);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var ticket = JsonSerializer.Deserialize<Ticket>(response, new JsonSerializerOptions
                {   
                    PropertyNameCaseInsensitive = true
                });

                if (ticket == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontró el ticket especificado.");
                    return View();
                }

                return View(ticket);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View();
            }
        }

        public async Task<IActionResult> AddTicketAsync(TicketDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _accountService.AddTicketAsync(model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                var ticket = JsonSerializer.Deserialize<Ticket>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return RedirectToAction("GetAllTicketAsync"); // Redirige a la lista de tickets
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al agregar el ticket: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> UpdateTicketAsync(int id, TicketDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _accountService.UpdateTicketAsync(id, model);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllTicketAsync");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el ticket: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteTicketAsync(int id)
        {

            try
            {
                var response = await _accountService.DeleteTicketAsync(id);
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllTicketAsync"); // Redirige a la lista de tickets
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al eliminar el ticket: {ex.Message}");
                return View();
            }
        }
    }
}
