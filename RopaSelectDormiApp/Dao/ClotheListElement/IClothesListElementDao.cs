using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.ClotheListElement;

namespace RopaSelectDormiApp.Dao.ClotheListElement;

public interface IClothesListElementDao
{
    Task<List<ClotheListElementModel>> FindClothesListElementsByClotheListId(long idClotheList);
    
    Task<List<ClotheListElementIdNameQuantityModel>> FindClothesListElementsIdNameQuantityByClotheListId(long idClotheList);

    Task AddClotheListElement(CreateClotheListElementDto createClotheListElement);

    Task DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe);

    Task<long> FindQuantityByListAndElementId(long idClotheList, long idClothe);

    Task UpdateClotheListElementQuantity(UpdateClotheListElementDto updateClotheListElement);
    
    Task<bool> ExistClotheElementInClotheListById(long idClotheList, long idClothe);
}