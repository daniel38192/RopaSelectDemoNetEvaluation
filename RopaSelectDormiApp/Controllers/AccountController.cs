using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RopaSelectDormiApp.Entities.User;

namespace RopaSelectDormiApp.Controllers;

[AllowAnonymous]
public class AccountController(SignInManager<User> signInManager , UserManager<User> userManager): Controller
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index() => View();
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewData["Message"] = "Username and password are required.";
            ViewData["ShowMessage"] = true;
            return View("Index");
        }

        var result = await signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: true);
            
        if (result.Succeeded)
        {
            return RedirectToLocal(returnUrl);
        }

        if (result.IsLockedOut)
        {
            ViewData["Message"] = "Account is locked. Please try again later.";
        }
        else if (result.RequiresTwoFactor)
        {
            ViewData["Message"] = "Two-factor authentication required.";
        }
        else if (result.IsNotAllowed)
        {
            ViewData["Message"] = "Login is not allowed. Please check your email to confirm your account.";
        }
        else
        {
            ViewData["Message"] = "Invalid username or password.";
        }
        
        ViewData["ShowMessage"] = true;
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return RedirectToAction("Index");
        }
        await signInManager.SignOutAsync();
        return RedirectToAction("Index");
    }
    
    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        return RedirectToAction("Index", "Home");
    }
    
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register() => View("Register");
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateAccount(string username, string password, string confirmPassword)
    {
        // Validate passwords match
        if (password != confirmPassword)
        {
            ViewData["Message"] = "Passwords do not match.";
            ViewData["ShowMessage"] = true;
            return View("Register");
        }

        var user = new User { UserName = username };
        var result = await userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            ViewData["Message"] = "Account created successfully. Please log in.";
            ViewData["ShowMessage"] = true;
            return View("Index");
        }
        
        ViewData["Message"] = "Failed to create account. " + string.Join("; ", result.Errors.Select(e => e.Description));
        ViewData["ShowMessage"] = true;
        return View("Register");
    }
}

