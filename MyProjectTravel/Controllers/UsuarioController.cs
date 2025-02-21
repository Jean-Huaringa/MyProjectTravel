using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProjectTravel.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsuarioController : Controller
    {

    }
}
