using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Service.Clothe;

namespace RopaSelectDormiApp.Controllers.Clothes;

[Authorize]
public class ClothesController(IClothesService clothesService) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }
        await SetClothesViewData(true, userId);
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description")] CreateClotheDto createClotheDto)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        if (ModelState.IsValid)
        {
            await AddClothe(createClotheDto, userId);
            return RedirectToAction(nameof(Index));
        }

        await SetClothesViewData(false, userId);
        return View(viewName:"Index", createClotheDto);
    }
    
    public async Task AddClothe(CreateClotheDto createClotheDto, Guid userId)
    {
        if (string.IsNullOrEmpty(createClotheDto.Description) 
            || string.IsNullOrWhiteSpace(createClotheDto.Description))
        {
            createClotheDto.Description = null;
        }
        await clothesService.AddClothe(createClotheDto, userId);
    }
    
    public async Task SetClothesViewData(bool hideAddClotheForm, Guid userId)
    {
        ViewData["clothes"] = await clothesService.FindClothes(userId);
        ViewData["hideAddClotheForm"] = hideAddClotheForm;
    }

    private bool TryGetCurrentUserId(out Guid userId)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(idValue, out userId);
    }
    
}
