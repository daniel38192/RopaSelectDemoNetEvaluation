using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheListElement;
using RopaSelectDormiApp.Service.ClotheList;
using RopaSelectDormiApp.Service.ClotheListElement;

namespace RopaSelectDormiApp.Controllers.ApiClothesList;

[ApiController]
[Route("[controller]")]
public class ApiClothesListController(IClothesListElementService clothesListElementService, IClothesListService clothesListService): Controller
{
    [HttpGet("FindById/{idClotheList:long}")]
    public async Task<ActionResult<IEnumerable<ClotheListElementModel>>> FindById(long idClotheList)
        => await clothesListElementService.FindClothesListElementsByClotheListId(idClotheList);

    [HttpGet("FindNameQuantityById/{idClotheList:long}")]
    public async Task<ActionResult<IEnumerable<ClotheListElementIdNameQuantityModel>>> FindNameQuantityById(
        long idClotheList)
        => await clothesListElementService.FindClothesListElementsNameQuantityByClotheListId(idClotheList);

    [HttpGet("FindClothesNotInListById/{idClotheList:long}")]
    public async Task<ActionResult<IEnumerable<ClotheModel>>> FindClothesNotInListById(long idClotheList)
        => await clothesListService.FindClothesThatNotAreInListYet(idClotheList);
    
    [HttpGet("CountTotal")]
    public async Task<ActionResult<object>> CountTotal()
        => new { total = await clothesListService.CountTotalAvailableLists()};
    
    [HttpGet("FindQuantityByListAndElementId/{idClotheList:long}/{idClothe:long}")]
    public async Task<ActionResult<object>> FindQuantityByListAndElementId(long idClotheList, long idClothe)
        => new { quantity = await clothesListElementService.FindQuantityByListAndElementId(idClotheList, idClothe) };


    [ValidateAntiForgeryToken]
    [HttpPatch("AddClotheToList")]
    public async Task<IActionResult> AddClothesToList([FromBody] CreateClotheListElementDto createClotheListElement)
    {
        if (createClotheListElement.InitialQuantity < 0)
        {
            return BadRequest("Cantidad inicial no puede ser negativo.");
        }
        await clothesListElementService.AddClotheListElement(createClotheListElement);
        return Ok();
    }

    [ValidateAntiForgeryToken]
    [HttpPatch("UpdateQuantity")]
    public async Task<IActionResult> UpdateClotheListElementQuantity([FromBody] UpdateClotheListElementDto updateClotheListElement)
    {
        if (!await clothesListElementService.ExistClotheElementInClotheListById(
                updateClotheListElement.PreviousIdClothesList,
                updateClotheListElement.PreviousIdClothes)
            )
            return NotFound();

        await clothesListElementService.UpdateClotheListElementQuantity(updateClotheListElement);
        return Ok();
    }
    
    [ValidateAntiForgeryToken]
    [HttpDelete("DeleteClotheListElementByListAndElementId/{idClotheList:long}/{idClothe:long}")]
    public async Task<IActionResult> DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe)
    {
        if (!await clothesListElementService.ExistClotheElementInClotheListById(idClotheList, idClothe))
            return NotFound();
        await clothesListElementService.DeleteClotheListElementByListAndElementId(idClotheList, idClothe);
        return Ok();

    }
    
}