using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Model.Clothe;

namespace RopaSelectDormiApp.Service.Clothe;

public interface IClothesService
{
    Task<ClotheModel?> FindClotheById(long id, Guid userId);
    Task<List<ClotheModel>> FindClothes(Guid userId);
    Task AddClothe(CreateClotheDto createClothe, Guid userId);
    Task DeleteClotheById(long id, Guid userId);
    Task<bool> ExistClotheById(long id, Guid userId);
}
