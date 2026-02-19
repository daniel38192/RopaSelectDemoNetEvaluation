using RopaSelectDormiApp.Dao.ClotheList;
using RopaSelectDormiApp.Dto.ClotheList;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheList;

namespace RopaSelectDormiApp.Service.ClotheList;

public class ClothesListServiceImpl(IClothesListDao clothesListDao): IClothesListService
{
    public Task AddClotheList(CreateClotheListDto createClothe) => clothesListDao.AddClotheList(createClothe);

    public Task<List<ClotheListModel>> FindAllClothesList() => clothesListDao.FindAllClothesList();

    public Task<List<ClotheListModel>> FindAllClothesListOrderedLimitOffset(long limit, long offset) =>
        clothesListDao.FindAllClothesListOrderedLimitOffset(limit, offset);

    public Task<long> CountTotalAvailableLists()
        => clothesListDao.CountTotalAvailableLists();

    public Task<List<ClotheModel>> FindClothesThatNotAreInListYet(long idClotheList)
        => clothesListDao.FindClothesThatNotAreInListYet(idClotheList);

    public Task DeleteClotheListById(long id) => clothesListDao.DeleteClotheListById(id);
}