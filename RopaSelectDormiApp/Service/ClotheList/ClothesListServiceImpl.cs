using RopaSelectDormiApp.Dao.ClotheList;
using RopaSelectDormiApp.Dto.ClotheList;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheList;

namespace RopaSelectDormiApp.Service.ClotheList;

public class ClothesListServiceImpl(IClothesListDao clothesListDao): IClothesListService
{
    public Task AddClotheList(CreateClotheListDto createClothe, Guid userId) =>
        clothesListDao.AddClotheList(createClothe, userId);

    public Task<List<ClotheListModel>> FindAllClothesList(Guid userId) => clothesListDao.FindAllClothesList(userId);

    public Task<List<ClotheListModel>> FindAllClothesListOrderedLimitOffset(long limit, long offset, Guid userId) =>
        clothesListDao.FindAllClothesListOrderedLimitOffset(limit, offset, userId);

    public Task<long> CountTotalAvailableLists(Guid userId)
        => clothesListDao.CountTotalAvailableLists(userId);

    public Task<List<ClotheModel>> FindClothesThatNotAreInListYet(long idClotheList, Guid userId)
        => clothesListDao.FindClothesThatNotAreInListYet(idClotheList, userId);

    public Task DeleteClotheListById(long id, Guid userId) => clothesListDao.DeleteClotheListById(id, userId);
}
