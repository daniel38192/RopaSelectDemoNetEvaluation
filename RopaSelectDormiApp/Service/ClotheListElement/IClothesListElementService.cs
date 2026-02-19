namespace RopaSelectDormiApp.Service.ClotheListElement;
using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.ClotheListElement;

public interface IClothesListElementService
{
    Task<List<ClotheListElementModel>> FindClothesListElementsByClotheListId(long idClotheList);
    
    Task<List<ClotheListElementIdNameQuantityModel>> FindClothesListElementsNameQuantityByClotheListId(long idClotheList);

    Task AddClotheListElement(CreateClotheListElementDto createClotheListElement);

    Task<long> FindQuantityByListAndElementId(long idClotheList, long idClothe);

    Task UpdateClotheListElementQuantity(UpdateClotheListElementDto updateClotheListElement);

    Task DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe);

    Task<bool> ExistClotheElementInClotheListById(long idClotheList, long idClothe);
}