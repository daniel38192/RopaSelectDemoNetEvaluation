using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Model.Clothe;

namespace RopaSelectDormiApp.Service.Clothe;

public interface IClothesService
{
    Task<ClotheModel?> FindClotheById(long id);
    Task<List<ClotheModel>> FindClothes();
    Task AddClothe(CreateClotheDto createClothe);
    Task DeleteClotheById(long id);
}