using System.Threading.Tasks;
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
        await AddClothesListToView(10, 0);
        return View();
    }
    
    [HttpGet("[controller]/Manage/Page/{pageNumber:long}")]
    public async Task<IActionResult> ManagePage(long pageNumber)
    {
        if (pageNumber < 0)
        {
            return BadRequest(new {error = "Invalid page number: "+pageNumber+", expected positive integer"});
        }
        await AddClothesListToView(10, pageNumber);
        return View(viewName: "Index");
    }

    [HttpPost("[controller]/Manage")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] CreateClotheListDto createClotheList)
    {
        if (ModelState.IsValid)
        {
            await AddClotheList(createClotheList);
            await AddClothesListToView(10, 0);
            return View(viewName: "Index");
        }

        await AddClothesListToView(10, 0);
        return View(viewName: "Index", createClotheList);
    }

    public async Task AddClothesListToView(long maxItems, long pageNumber)
    {
        ViewData["clothesList"] = await clothesListService.FindAllClothesListOrderedLimitOffset(maxItems, pageNumber*maxItems);
    }

    private async Task AddClotheList(CreateClotheListDto createClotheListDto)
    {
        createClotheListDto.Name = IsUserInputIgnored(createClotheListDto.Name) ? null : createClotheListDto.Name;
        await clothesListService.AddClotheList(createClotheListDto);
    }

    private static bool IsUserInputIgnored(string? compare)
    {
        return string.IsNullOrEmpty(compare) || string.IsNullOrWhiteSpace(compare);
    }
}