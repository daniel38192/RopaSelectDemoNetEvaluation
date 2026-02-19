

using RopaSelectDormiApp.Dao.ClotheListElement;
using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.ClotheListElement;

namespace RopaSelectDormiApp.Service.ClotheListElement;

public class ClothesListElementServiceImpl(IClothesListElementDao clothesListElementDao): IClothesListElementService
{
    public Task<List<ClotheListElementModel>> FindClothesListElementsByClotheListId(long idClotheList)
        => clothesListElementDao.FindClothesListElementsByClotheListId(idClotheList);

    public Task<List<ClotheListElementIdNameQuantityModel>> FindClothesListElementsNameQuantityByClotheListId(
        long idClotheList)
        => clothesListElementDao.FindClothesListElementsIdNameQuantityByClotheListId(idClotheList);

    public Task AddClotheListElement(CreateClotheListElementDto createClotheListElement)
        => clothesListElementDao.AddClotheListElement(createClotheListElement);

    public Task<long> FindQuantityByListAndElementId(long idClotheList, long idClothe)
        => clothesListElementDao.FindQuantityByListAndElementId(idClotheList, idClothe);

    public Task UpdateClotheListElementQuantity(UpdateClotheListElementDto updateClotheListElement)
        => clothesListElementDao.UpdateClotheListElementQuantity(updateClotheListElement);

    public Task DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe)
        => clothesListElementDao.DeleteClotheListElementByListAndElementId(idClotheList, idClothe);

    public Task<bool> ExistClotheElementInClotheListById(long idClotheList, long idClothe)
        => clothesListElementDao.ExistClotheElementInClotheListById(idClotheList, idClothe);
}