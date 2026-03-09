using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RopaSelectDormiApp.Dto.ClotheList;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheList;

namespace RopaSelectDormiApp.Service.ClotheList;

public interface IClothesListService
{
    Task AddClotheList(CreateClotheListDto createClothe, Guid userId);
    Task<List<ClotheListModel>> FindAllClothesList(Guid userId);
    Task<List<ClotheListModel>> FindAllClothesListOrderedLimitOffset(long limit, long offset, Guid userId);
    Task<long> CountTotalAvailableLists(Guid userId);
    Task<List<ClotheModel>> FindClothesThatNotAreInListYet(long clotheListId, Guid userId);
    Task DeleteClotheListById(long id, Guid userId);
}
