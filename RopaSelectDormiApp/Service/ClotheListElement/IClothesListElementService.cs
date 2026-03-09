using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RopaSelectDormiApp.Service.ClotheListElement;
using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.ClotheListElement;

public interface IClothesListElementService
{
    Task<List<ClotheListElementModel>> FindClothesListElementsByClotheListId(long idClotheList, Guid userId);
    
    Task<List<ClotheListElementIdNameQuantityModel>> FindClothesListElementsNameQuantityByClotheListId(long idClotheList, Guid userId);

    Task AddClotheListElement(CreateClotheListElementDto createClotheListElement, Guid userId);

    Task<long> FindQuantityByListAndElementId(long idClotheList, long idClothe, Guid userId);

    Task UpdateClotheListElementQuantity(UpdateClotheListElementDto updateClotheListElement, Guid userId);

    Task DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe, Guid userId);

    Task<bool> ExistClotheElementInClotheListById(long idClotheList, long idClothe, Guid userId);
}
