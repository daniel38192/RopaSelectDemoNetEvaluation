using RopaSelectDormiApp.Dao.Clothe;
using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Model.Clothe;

namespace RopaSelectDormiApp.Service.Clothe;

public class ClothesServiceImpl(IClothesDao clothesDao): IClothesService
{
    public Task<ClotheModel?> FindClotheById(long id) => clothesDao.FindClotheById(id);

    public Task<List<ClotheModel>> FindClothes() => clothesDao.FindAllClothes();

    public Task AddClothe(CreateClotheDto createClothe) => clothesDao.AddClothe(createClothe);

    public Task DeleteClotheById(long id) => clothesDao.DeleteClotheById(id);
}