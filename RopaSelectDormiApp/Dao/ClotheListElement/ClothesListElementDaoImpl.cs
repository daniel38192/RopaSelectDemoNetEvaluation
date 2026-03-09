using Npgsql;
using RopaSelectDormiApp.Dto.ClotheListElement;
using RopaSelectDormiApp.Model.ClotheListElement;
using static RopaSelectDormiApp.RowMapper.RowMapper;

namespace RopaSelectDormiApp.Dao.ClotheListElement;

public class ClothesListElementDaoImpl(NpgsqlDataSource dataSource): IClothesListElementDao
{
    public async Task<List<ClotheListElementModel>> FindClothesListElementsByClotheListId(long idClotheList, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT id_clothes, quantity FROM clothes_list_elements WHERE id_clothes_list = @idClotheList AND user_id = @userId"
            );

        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("userId", userId);
        
        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheListElementModelRowMapper);
    }

    public async Task<List<ClotheListElementIdNameQuantityModel>> FindClothesListElementsIdNameQuantityByClotheListId(
        long idClotheList, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT clothes.id, clothes.name, clothes_list_elements.quantity FROM clothes_list_elements " +
            "JOIN clothes ON clothes_list_elements.id_clothes = clothes.id " +
            "WHERE id_clothes_list = @idClotheList AND clothes_list_elements.user_id = @userId"
        );

        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("userId", userId);

        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheListElementIdNameQuantityModelRowMapper);
    }

    public async Task AddClotheListElement(CreateClotheListElementDto createClotheListElement, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "INSERT INTO clothes_list_elements (user_id, id_clothes_list, id_clothes, quantity) VALUES (@userId, @idClotheList, @idClothes, @quantity)"
            );
        AddParametersCreateClotheListElement(cmd, createClotheListElement, userId);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteClotheListElementByListAndElementId(long idClotheList, long idClothe, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "DELETE FROM clothes_list_elements WHERE id_clothes_list = @idClotheList AND id_clothes = @idClothe AND user_id = @userId"
            );
        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("idClothe", idClothe);
        cmd.Parameters.AddWithValue("userId", userId);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<long> FindQuantityByListAndElementId(long idClotheList, long idClothe, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT quantity FROM clothes_list_elements WHERE id_clothes_list = @idClotheList AND id_clothes = @idClothe AND user_id = @userId"
            );
        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("idClothe", idClothe);
        cmd.Parameters.AddWithValue("userId", userId);
        await using var reader = await cmd.ExecuteReaderAsync();
        return await MapResultSetToObject(reader, dataReader => dataReader.GetInt64(0));
    }

    private static void AddParametersCreateClotheListElement(
        NpgsqlCommand cmd,
        CreateClotheListElementDto createClotheListElement,
        Guid userId)
    {
        cmd.Parameters.AddWithValue("userId", userId);
        cmd.Parameters.AddWithValue("idClotheList", createClotheListElement.IdClothesList);
        cmd.Parameters.AddWithValue("idClothes", createClotheListElement.IdClothes);
        cmd.Parameters.AddWithValue("quantity", createClotheListElement.InitialQuantity);
    }

    public async Task UpdateClotheListElementQuantity(UpdateClotheListElementDto updateClotheListElement, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "UPDATE clothes_list_elements SET quantity = @quantity WHERE id_clothes_list = @idClotheList AND id_clothes = @idClothes AND user_id = @userId"
            );
        AddParametersUpdateClotheListElement(cmd, updateClotheListElement, userId);
        await cmd.ExecuteNonQueryAsync();
    }
    
    private static void AddParametersUpdateClotheListElement(
        NpgsqlCommand cmd,
        UpdateClotheListElementDto updateClotheListElement,
        Guid userId)
    {
        cmd.Parameters.AddWithValue("userId", userId);
        cmd.Parameters.AddWithValue("idClotheList", updateClotheListElement.PreviousIdClothesList);
        cmd.Parameters.AddWithValue("idClothes", updateClotheListElement.PreviousIdClothes);
        cmd.Parameters.AddWithValue("quantity", updateClotheListElement.NewQuantity);
    }

    public async Task<bool> ExistClotheElementInClotheListById(long idClotheList, long idClothe, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT EXISTS(SELECT * FROM clothes_list_elements WHERE id_clothes_list = @idClotheList AND id_clothes = @idClothe AND user_id = @userId)"
            );
        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("idClothe", idClothe);
        cmd.Parameters.AddWithValue("userId", userId);
        await using var reader = await cmd.ExecuteReaderAsync();
        return await MapResultSetToObject(reader, dataReader => dataReader.GetBoolean(0));
    }
}
