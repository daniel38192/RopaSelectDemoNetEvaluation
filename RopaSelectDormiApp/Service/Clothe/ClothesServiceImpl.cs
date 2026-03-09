using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RopaSelectDormiApp.Dao.Clothe;
using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Model.Clothe;

namespace RopaSelectDormiApp.Service.Clothe;

public class ClothesServiceImpl(IClothesDao clothesDao): IClothesService
{
    public Task<ClotheModel?> FindClotheById(long id, Guid userId) => clothesDao.FindClotheById(id, userId);

    public Task<List<ClotheModel>> FindClothes(Guid userId) => clothesDao.FindAllClothes(userId);

    public Task AddClothe(CreateClotheDto createClothe, Guid userId) => clothesDao.AddClothe(createClothe, userId);

    public Task DeleteClotheById(long id, Guid userId) => clothesDao.DeleteClotheById(id, userId);
    
    public Task<bool> ExistClotheById(long id, Guid userId) => clothesDao.ExistClotheById(id, userId);
}
