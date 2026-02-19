using Microsoft.AspNetCore.Mvc;
using RopaSelectDormiApp.Service.User;

namespace RopaSelectDormiApp.Controllers.Users;

public class UsersController(IUserService userService): Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Users"] = await userService.FindAllAsync();
        return View();
    }
}