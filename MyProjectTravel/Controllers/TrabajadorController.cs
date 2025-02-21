using Microsoft.AspNetCore.Authorization;

namespace MyProjectTravel.Controllers
{
    [Authorize(Roles = "admin")]
    public class TrabajadorController
    {

    }
}
