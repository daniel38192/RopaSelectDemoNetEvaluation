using System.Threading.Tasks;
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
        await SetClothesViewData(true);
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description")] CreateClotheDto createClotheDto)
    {
        if (ModelState.IsValid)
        {
            await AddClothe(createClotheDto);
            return RedirectToAction(nameof(Index));
        }

        await SetClothesViewData(false);
        return View(viewName:"Index", createClotheDto);
    }
    
    public async Task AddClothe(CreateClotheDto createClotheDto)
    {
        if (string.IsNullOrEmpty(createClotheDto.Description) 
            || string.IsNullOrWhiteSpace(createClotheDto.Description))
        {
            createClotheDto.Description = null;
        }
        await clothesService.AddClothe(createClotheDto);
    }
    
    public async Task SetClothesViewData(bool hideAddClotheForm)
    {
        ViewData["clothes"] = await clothesService.FindClothes();
        ViewData["hideAddClotheForm"] = hideAddClotheForm;
    }
    
}