using System.Collections.Generic;
using System.Threading.Tasks;
using RopaSelectDormiApp.Dto.ClotheList;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheList;

namespace RopaSelectDormiApp.Dao.ClotheList;

public interface IClothesListDao
{
    Task AddClotheList(CreateClotheListDto createClothe);
    Task<List<ClotheListModel>> FindAllClothesList();
    Task<List<ClotheListModel>> FindAllClothesListOrderedLimitOffset(long limit, long offset);
    Task<long> CountTotalAvailableLists();
    Task<List<ClotheModel>> FindClothesThatNotAreInListYet(long clotheListId);
    Task DeleteClotheListById(long id);
}