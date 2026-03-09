

using RopaSelectDormiApp.Dao.ClotheListElement;
using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.ClotheListElement;

namespace RopaSelectDormiApp.Service.ClotheListElement;

public class ClothesListElementServiceImpl(IClothesListElementDao clothesListElementDao): IClothesListElementService
{
    public Task<List<ClotheListElementModel>> FindClothesListElementsByClotheListId(long idClotheList, Guid userId)
        => clothesListElementDao.FindClothesListElementsByClotheListId(idClotheList, userId);

    public Task<List<ClotheListElementIdNameQuantityModel>> FindClothesListElementsNameQuantityByClotheListId(
        long idClotheList, Guid userId)
        => clothesListElementDao.FindClothesListElementsIdNameQuantityByClotheListId(idClotheList, userId);

    public Task AddClotheListElement(CreateClotheListElementDto createClotheListElement, Guid userId)
        => clothesListElementDao.AddClotheListElement(createClotheListElement, userId);

    public Task<long> FindQuantityByListAndElementId(long idClotheList, long idClothe, Guid userId)
        => clothesListElementDao.FindQuantityByListAndElementId(idClotheList, idClothe, userId);

    public Task UpdateClotheListElementQuantity(UpdateClotheListElementDto updateClotheListElement, Guid userId)
        => clothesListElementDao.UpdateClotheListElementQuantity(updateClotheListElement, userId);

    public Task DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe, Guid userId)
        => clothesListElementDao.DeleteClotheListElementByListAndElementId(idClotheList, idClothe, userId);

    public Task<bool> ExistClotheElementInClotheListById(long idClotheList, long idClothe, Guid userId)
        => clothesListElementDao.ExistClotheElementInClotheListById(idClotheList, idClothe, userId);
}
