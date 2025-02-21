using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyProjectTravel.Models.Acount;
using MyProjectTravel.Models.DTO;
using MyProjectTravel.Models;
using MyProyectTravel.Services.Public;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Net.Http;

namespace MyProjectTravel.Controllers.Public
{

    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var response = await _accountService.LoginAsync(model);

            if (response == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            var user = JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions
            {   
                PropertyNameCaseInsensitive = true
            });
    
            User useResponse = user;

            var role = user.worker?.role ?? "invitado";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, useResponse.userName),
                new Claim(ClaimTypes.Email, useResponse.mail),
                new Claim(ClaimTypes.Email, user.mail),
                new Claim(ClaimTypes.Role, role)
            };


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO model)
        {
            var response = await _accountService.RegisterAsync(model);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var response = await _accountService.LogoutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser()
        {
            var response = await _accountService.GetUser();

            if (response == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }
            var user = JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "No se encontró el Bus especificado.");
                return View();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDTO model)
        {
            var response = await _accountService.UpdateUserAsync(model);
            return RedirectToAction("Account", "GetUser");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(PasswordUpdateRequest model)
        {
            var response = await _accountService.UpdatePasswordAsync(model);
            return RedirectToAction("Account", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var response = await _accountService.GetUser();

            if (response == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }
            var user = JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "No se encontró el Bus especificado.");
                return View();
            }

            return View(user);
        }

    }
}
