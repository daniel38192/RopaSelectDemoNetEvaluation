using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.ClotheListElement;

namespace RopaSelectDormiApp.Dao.ClotheListElement;

public interface IClothesListElementDao
{
    Task<List<ClotheListElementModel>> FindClothesListElementsByClotheListId(long idClotheList, Guid userId);
    
    Task<List<ClotheListElementIdNameQuantityModel>> FindClothesListElementsIdNameQuantityByClotheListId(long idClotheList, Guid userId);

    Task AddClotheListElement(CreateClotheListElementDto createClotheListElement, Guid userId);

    Task DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe, Guid userId);

    Task<long> FindQuantityByListAndElementId(long idClotheList, long idClothe, Guid userId);

    Task UpdateClotheListElementQuantity(UpdateClotheListElementDto updateClotheListElement, Guid userId);
    
    Task<bool> ExistClotheElementInClotheListById(long idClotheList, long idClothe, Guid userId);
}
