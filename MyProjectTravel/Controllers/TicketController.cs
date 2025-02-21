using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models;
using MyProjectTravel.Models.DTO;
using MyProyectTravel.Services;
using System.Text.Json;

namespace MyProjectTravel.Controllers
{
    [Route("Ticket")]
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTicketAsync()
        {
            try
            {
                var response = await _ticketService.GetAllTicketAsync();
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

                return View("GetAll", tickets);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error de comunicación con la API: {ex.Message}");
                return View(new List<Ticket>());
            }
        }

        [HttpGet("GetTicketById")]
        public async Task<IActionResult> GetTicketByIdAsync(int id)
        {
            try
            {
                var response = await _ticketService.GetTicketByIdAsync(id);
                
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

        [HttpGet("AddTicket")]
        public async Task<IActionResult> AddTicketAsync()
        {
            return View(new Ticket());
        }

        [HttpPost("AddTicket")]
        public async Task<IActionResult> AddTicketAsync(Ticket TicketModel)
        {
            if (!ModelState.IsValid)
            {
                return View(TicketModel); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _ticketService.AddTicketAsync(TicketModel);
                
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
                return View(TicketModel);
            }
        }

        [HttpGet("UpdateTicket")]
        public async Task<IActionResult> UpdateTicketAsync(int id)
        {
            var response = await _ticketService.GetTicketByIdAsync(id);

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

        [HttpPost("UpdateTicket")]
        public async Task<IActionResult> UpdateTicketAsync(int id, Ticket TicketModel)
        {
            if (!ModelState.IsValid)
            {
                return View(TicketModel); // Retorna la vista con los errores de validación
            }

            try
            {
                var response = await _ticketService.UpdateTicketAsync(id, TicketModel);
                
                if (response == null)
                {
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                return RedirectToAction("GetAllTicketAsync");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar el ticket: {ex.Message}");
                return View(TicketModel);
            }
        }

        [HttpPost("DeleteTicket")]
        public async Task<IActionResult> DeleteTicketAsync(int id)
        {

            try
            {
                var response = await _ticketService.DeleteTicketAsync(id);
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
