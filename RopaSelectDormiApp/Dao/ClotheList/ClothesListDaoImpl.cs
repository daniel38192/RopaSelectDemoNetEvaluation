using Npgsql;
using RopaSelectDormiApp.Dto.ClotheList;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheList;
using static RopaSelectDormiApp.RowMapper.RowMapper;

namespace RopaSelectDormiApp.Dao.ClotheList;

public class ClothesListDaoImpl(NpgsqlDataSource dataSource): IClothesListDao
{
    public async Task AddClotheList(CreateClotheListDto createClotheList, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "INSERT INTO clothes_list (user_id, name, created_at) VALUES  (@userId, @name, @createdAt)");

        cmd.Parameters.AddWithValue("userId", userId);
        cmd.Parameters.AddWithValue("name", createClotheList.Name != null ? createClotheList.Name : DBNull.Value);
        cmd.Parameters.AddWithValue("createdAt", DateTime.Now);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<ClotheListModel>> FindAllClothesList(Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT id, name, created_at FROM clothes_list WHERE user_id = @userId");
        cmd.Parameters.AddWithValue("userId", userId);

        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheListModelRowMapper);
    }

    public async Task<List<ClotheListModel>> FindAllClothesListOrderedLimitOffset(long limit, long offset, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT id, name, created_at FROM clothes_list WHERE user_id = @userId ORDER BY created_at DESC LIMIT @limit OFFSET @offset");

        cmd.Parameters.AddWithValue("userId", userId);
        cmd.Parameters.AddWithValue("limit", limit);
        cmd.Parameters.AddWithValue("offset", offset);

        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheListModelRowMapper);
    }

    public async Task<long> CountTotalAvailableLists(Guid userId)
    {
        await using var cmd = dataSource.CreateCommand("SELECT COUNT(id) FROM clothes_list WHERE user_id = @userId");
        cmd.Parameters.AddWithValue("userId", userId);
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        return reader.GetInt64(0);
    }

    public async Task<List<ClotheModel>> FindClothesThatNotAreInListYet(long idClotheList, Guid userId)
    {
        const string sqlCommand = "SELECT clothes.id, clothes.name, clothes.description FROM clothes LEFT JOIN " +
                                  "(SELECT clothes_list_elements.id_clothes FROM clothes_list_elements WHERE id_clothes_list = @idClotheList AND user_id = @userId) " +
                                  "AS vv ON vv.id_clothes = clothes.id WHERE vv.id_clothes IS NULL AND clothes.user_id = @userId";
        await using var cmd = dataSource.CreateCommand(sqlCommand);
        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        cmd.Parameters.AddWithValue("userId", userId);
        await using var reader = await cmd.ExecuteReaderAsync();
        return await MapResultSetToList(reader, ClotheRowMapper);
    }

    public async Task DeleteClotheListById(long id, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand("DELETE FROM clothes_list WHERE id = @id AND user_id = @userId");
        cmd.Parameters.AddWithValue("id", id);
        cmd.Parameters.AddWithValue("userId", userId);
        await cmd.ExecuteNonQueryAsync();
    }
}
