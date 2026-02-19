using Npgsql;
using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.ClotheListElement;
using static RopaSelectDormiApp.RowMapper.RowMapper;

namespace RopaSelectDormiApp.Dao.ClotheListElement;

public class ClothesListElementDaoImpl(NpgsqlDataSource dataSource): IClothesListElementDao
{
    public async Task<List<ClotheListElementModel>> FindClothesListElementsByClotheListId(long idClotheList)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT id_clothes, quantity FROM clothes_list_elements WHERE id_clothes_list = @idClotheList"
            );

        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        
        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheListElementModelRowMapper);
    }

    public async Task<List<ClotheListElementIdNameQuantityModel>> FindClothesListElementsIdNameQuantityByClotheListId(long idClotheList)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT clothes.id, clothes.name, clothes_list_elements.quantity FROM clothes_list_elements " +
            "JOIN clothes ON clothes_list_elements.id_clothes = clothes.id " +
            "WHERE id_clothes_list = @idClotheList"
        );

        cmd.Parameters.AddWithValue("idClotheList", idClotheList);

        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheListElementIdNameQuantityModelRowMapper);
    }

    public async Task AddClotheListElement(CreateClotheListElementDto createClotheListElement)
    {
        await using var cmd = dataSource.CreateCommand(
            "INSERT INTO clothes_list_elements (id_clothes_list, id_clothes, quantity) VALUES (@idClotheList, @idClothes, @quantity)"
            );
        AddParametersCreateClotheListElement(cmd, createClotheListElement);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe)
    {
        await using var cmd = dataSource.CreateCommand(
            "DELETE FROM clothes_list_elements WHERE id_clothes_list = @idClotheList AND id_clothes = @idClothe"
            );
        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("idClothe", idClothe);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<long> FindQuantityByListAndElementId(long idClotheList, long idClothe)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT quantity FROM clothes_list_elements WHERE id_clothes_list = @idClotheList AND id_clothes = @idClothe"
            );
        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("idClothe", idClothe);
        await using var reader = await cmd.ExecuteReaderAsync();
        return await MapResultSetToObject(reader, dataReader => dataReader.GetInt64(0));
    }

    private static void AddParametersCreateClotheListElement(NpgsqlCommand cmd, CreateClotheListElementDto createClotheListElement)
    {
        cmd.Parameters.AddWithValue("idClotheList", createClotheListElement.IdClothesList);
        cmd.Parameters.AddWithValue("idClothes", createClotheListElement.IdClothes);
        cmd.Parameters.AddWithValue("quantity", createClotheListElement.InitialQuantity);
    }

    public async Task UpdateClotheListElementQuantity(UpdateClotheListElementDto updateClotheListElement)
    {
        await using var cmd = dataSource.CreateCommand(
            "UPDATE clothes_list_elements SET quantity = @quantity WHERE id_clothes_list = @idClotheList AND id_clothes = @idClothes"
            );
        AddParametersUpdateClotheListElement(cmd, updateClotheListElement);
        await cmd.ExecuteNonQueryAsync();
    }
    
    private static void AddParametersUpdateClotheListElement(NpgsqlCommand cmd, UpdateClotheListElementDto updateClotheListElement)
    {
        cmd.Parameters.AddWithValue("idClotheList", updateClotheListElement.PreviousIdClothesList);
        cmd.Parameters.AddWithValue("idClothes", updateClotheListElement.PreviousIdClothes);
        cmd.Parameters.AddWithValue("quantity", updateClotheListElement.NewQuantity);
    }

    public async Task<bool> ExistClotheElementInClotheListById(long idClotheList, long idClothe)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT EXISTS(SELECT * FROM clothes_list_elements WHERE id_clothes_list = @idClotheList AND id_clothes = @idClothe)"
            );
        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("idClothe", idClothe);
        await using var reader = await cmd.ExecuteReaderAsync();
        return await MapResultSetToObject(reader, dataReader => dataReader.GetBoolean(0));
    }
}