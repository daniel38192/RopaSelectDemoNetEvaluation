using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopaSelectDormiApp.Service.User;

namespace RopaSelectDormiApp.Controllers.Users;

[Authorize(Roles = "ADMIN")]
public class UsersController(IUserService userService): Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Users"] = await userService.FindAllAsync();
        return View();
    }
}