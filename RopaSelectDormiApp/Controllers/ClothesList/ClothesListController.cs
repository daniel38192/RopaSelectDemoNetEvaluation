using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopaSelectDormiApp.Dto.ClotheList;
using RopaSelectDormiApp.Service.ClotheList;

namespace RopaSelectDormiApp.Controllers.ClothesList;

[Authorize]
public class ClothesListController(IClothesListService clothesListService) : Controller
{
    
    public async Task<IActionResult> Index()
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }
        await AddClothesListToView(10, 0, userId);
        return View();
    }
    
    [HttpGet("[controller]/Manage/Page/{pageNumber:long}")]
    public async Task<IActionResult> ManagePage(long pageNumber)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }
        if (pageNumber < 0)
        {
            return BadRequest(new {error = "Invalid page number: "+pageNumber+", expected positive integer"});
        }
        await AddClothesListToView(10, pageNumber, userId);
        return View(viewName: "Index");
    }

    [HttpPost("[controller]/Manage")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] CreateClotheListDto createClotheList)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        if (ModelState.IsValid)
        {
            await AddClotheList(createClotheList, userId);
            await AddClothesListToView(10, 0, userId);
            return View(viewName: "Index");
        }

        await AddClothesListToView(10, 0, userId);
        return View(viewName: "Index", createClotheList);
    }

    public async Task AddClothesListToView(long maxItems, long pageNumber, Guid userId)
    {
        ViewData["clothesList"] = await clothesListService.FindAllClothesListOrderedLimitOffset(maxItems, pageNumber*maxItems, userId);
    }

    private async Task AddClotheList(CreateClotheListDto createClotheListDto, Guid userId)
    {
        createClotheListDto.Name = IsUserInputIgnored(createClotheListDto.Name) ? null : createClotheListDto.Name;
        await clothesListService.AddClotheList(createClotheListDto, userId);
    }

    private static bool IsUserInputIgnored(string? compare)
    {
        return string.IsNullOrEmpty(compare) || string.IsNullOrWhiteSpace(compare);
    }

    private bool TryGetCurrentUserId(out Guid userId)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(idValue, out userId);
    }
}
