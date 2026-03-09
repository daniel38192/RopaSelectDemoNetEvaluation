using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheListElement;
using RopaSelectDormiApp.Service.Clothe;
using RopaSelectDormiApp.Service.ClotheList;
using RopaSelectDormiApp.Service.ClotheListElement;

namespace RopaSelectDormiApp.Controllers.ApiClothesList;

[ApiController]
[Route("[controller]")]
public class ApiClothesListController(IClothesListElementService clothesListElementService, IClothesListService clothesListService, IClothesService clothesService): Controller
{
    [HttpGet("FindById/{idClotheList:long}")]
    public async Task<ActionResult<IEnumerable<ClotheListElementModel>>> FindById(long idClotheList)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        return await clothesListElementService.FindClothesListElementsByClotheListId(idClotheList, userId);
    }

    [HttpGet("FindNameQuantityById/{idClotheList:long}")]
    public async Task<ActionResult<IEnumerable<ClotheListElementIdNameQuantityModel>>> FindNameQuantityById(
        long idClotheList)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        return await clothesListElementService.FindClothesListElementsNameQuantityByClotheListId(idClotheList, userId);
    }

    [HttpGet("FindClothesNotInListById/{idClotheList:long}")]
    public async Task<ActionResult<IEnumerable<ClotheModel>>> FindClothesNotInListById(long idClotheList)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        return await clothesListService.FindClothesThatNotAreInListYet(idClotheList, userId);
    }
    
    [HttpGet("CountTotal")]
    public async Task<ActionResult<object>> CountTotal()
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        return new { total = await clothesListService.CountTotalAvailableLists(userId)};
    }
    
    [HttpGet("FindQuantityByListAndElementId/{idClotheList:long}/{idClothe:long}")]
    public async Task<ActionResult<object>> FindQuantityByListAndElementId(long idClotheList, long idClothe)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        return new { quantity = await clothesListElementService.FindQuantityByListAndElementId(idClotheList, idClothe, userId) };
    }


    [ValidateAntiForgeryToken]
    [HttpPatch("AddClotheToList")]
    public async Task<IActionResult> AddClothesToList([FromBody] CreateClotheListElementDto createClotheListElement)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        if (createClotheListElement.InitialQuantity < 0)
        {
            return BadRequest("Cantidad inicial no puede ser negativo.");
        }
        var exists = await clothesService.ExistClotheById(createClotheListElement.IdClothes, userId);
        if (!exists)
        {
            return NotFound();
        }
        await clothesListElementService.AddClotheListElement(createClotheListElement, userId);
        return Ok();
    }

    [ValidateAntiForgeryToken]
    [HttpPatch("UpdateQuantity")]
    public async Task<IActionResult> UpdateClotheListElementQuantity([FromBody] UpdateClotheListElementDto updateClotheListElement)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        if (!await clothesListElementService.ExistClotheElementInClotheListById(
                updateClotheListElement.PreviousIdClothesList,
                updateClotheListElement.PreviousIdClothes,
                userId)
            )
            return NotFound();

        await clothesListElementService.UpdateClotheListElementQuantity(updateClotheListElement, userId);
        return Ok();
    }
    
    [ValidateAntiForgeryToken]
    [HttpDelete("DeleteClotheListElementByListAndElementId/{idClotheList:long}/{idClothe:long}")]
    public async Task<IActionResult> DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        if (!await clothesListElementService.ExistClotheElementInClotheListById(idClotheList, idClothe, userId))
            return NotFound();
        await clothesListElementService.DeleteClotheListElementByListAndElementId(idClotheList, idClothe, userId);
        return Ok();

    }

    private bool TryGetCurrentUserId(out Guid userId)
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(idValue, out userId);
    }
    
}
