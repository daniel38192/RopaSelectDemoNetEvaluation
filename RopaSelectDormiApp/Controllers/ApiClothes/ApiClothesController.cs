using Microsoft.AspNetCore.Mvc;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Service.Clothe;

namespace RopaSelectDormiApp.Controllers.ApiClothes;

[ApiController]
[Route("[controller]")]
public class ApiClothesController(IClothesService clothesService): Controller
{
    [HttpGet("FindAll")]
    public async Task<ActionResult<IEnumerable<ClotheModel>>> FindAll() => await clothesService.FindClothes();
    
    [ValidateAntiForgeryToken]
    [HttpDelete("Delete/{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var clothe = await clothesService.FindClotheById(id);
        if (clothe == null)
        {
            return NotFound();
        }
        await clothesService.DeleteClotheById(id);
        return Ok();
    }
}