using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models.DTO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyProjectTravel.Controllers.Public
{
    public class PasajesController : Controller
    {
        private readonly HttpClient _httpClient;

        public PasajesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyAPI");
        }

        [HttpGet]
        public async Task<IActionResult> Estaciones()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Pasajes/all/station");

                if (response.IsSuccessStatusCode)
                {
                    var estacionesJson = await response.Content.ReadAsStringAsync();
                    var estaciones = JsonConvert.DeserializeObject<List<dynamic>>(estacionesJson);
                    return View(estaciones);
                }

                ModelState.AddModelError(string.Empty, "Error al obtener las estaciones.");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error de comunicación con la API.");
            }

            return View(new List<dynamic>());
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(int? idOrigen, int? idDestino, DateTime? fechaInicio)
        {
            try
            {
                var data = new
                {
                    idOrigen,
                    idDestino,
                    fechaInicio
                };

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Pasajes/filter-for-passage", content);

                if (response.IsSuccessStatusCode)
                {
                    var itinerariosJson = await response.Content.ReadAsStringAsync();
                    var itinerarios = JsonConvert.DeserializeObject<List<dynamic>>(itinerariosJson);
                    return View(itinerarios);
                }

                ModelState.AddModelError(string.Empty, "Error al buscar itinerarios.");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error de comunicación con la API.");
            }

            return View(new List<dynamic>());
        }

        [HttpPost]
        public async Task<IActionResult> Informacion(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Pasajes/search-for-information-passage/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var infoJson = await response.Content.ReadAsStringAsync();
                    var informacion = JsonConvert.DeserializeObject<dynamic>(infoJson);
                    return View(informacion);
                }

                ModelState.AddModelError(string.Empty, "Error al obtener la información del pasaje.");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error de comunicación con la API.");
            }

            return RedirectToAction("Estaciones");
        }

        [HttpPost]
        public async Task<IActionResult> Comprar(TicketDTO ticket)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Pasajes/buy-ticket/", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Boleto comprado exitosamente.";
                    return RedirectToAction("Estaciones");
                }

                ModelState.AddModelError(string.Empty, "Error al realizar la compra del boleto.");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error de comunicación con la API.");
            }

            return View(ticket);
        }
    }
}
