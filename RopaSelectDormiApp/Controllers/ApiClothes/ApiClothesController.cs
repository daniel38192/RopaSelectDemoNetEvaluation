using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Service.Clothe;

namespace RopaSelectDormiApp.Controllers.ApiClothes;

[ApiController]
[Route("[controller]")]
public class ApiClothesController(IClothesService clothesService): Controller
{
    [HttpGet("FindAll")]
    public async Task<ActionResult<IEnumerable<ClotheModel>>> FindAll()
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        return await clothesService.FindClothes(userId);
    }
    
    [ValidateAntiForgeryToken]
    [HttpDelete("Delete/{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        var clothe = await clothesService.FindClotheById(id, userId);
        if (clothe == null)
        {
            return NotFound();
        }
        await clothesService.DeleteClotheById(id, userId);
        return Ok();
    }

    private bool TryGetCurrentUserId(out Guid userId)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(idValue, out userId);
    }
}
