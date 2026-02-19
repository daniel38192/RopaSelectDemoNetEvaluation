using RopaSelectDormiApp.Dto.ClotheList;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheList;

namespace RopaSelectDormiApp.Service.ClotheList;

public interface IClothesListService
{
    Task AddClotheList(CreateClotheListDto createClothe);
    Task<List<ClotheListModel>> FindAllClothesList();
    Task<List<ClotheListModel>> FindAllClothesListOrderedLimitOffset(long limit, long offset);
    Task<long> CountTotalAvailableLists();
    Task<List<ClotheModel>> FindClothesThatNotAreInListYet(long clotheListId);
    Task DeleteClotheListById(long id);
}