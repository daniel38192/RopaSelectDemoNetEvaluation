

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Model.Clothe;

namespace RopaSelectDormiApp.Dao.Clothe;

public interface IClothesDao
{
    Task<ClotheModel?> FindClotheById(long id, Guid userId);
    Task<List<ClotheModel>> FindAllClothes(Guid userId);
    Task AddClothe(CreateClotheDto createClothe, Guid userId);
    Task DeleteClotheById(long id, Guid userId);
    Task<bool> ExistClotheById(long id, Guid userId);
}
