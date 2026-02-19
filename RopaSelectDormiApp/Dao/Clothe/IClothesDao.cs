

using System.Collections.Generic;
using System.Threading.Tasks;
using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Model.Clothe;

namespace RopaSelectDormiApp.Dao.Clothe;

public interface IClothesDao
{
    Task<ClotheModel?> FindClotheById(long id);
    Task<List<ClotheModel>> FindAllClothes();
    Task AddClothe(CreateClotheDto createClothe);
    Task DeleteClotheById(long id);
}