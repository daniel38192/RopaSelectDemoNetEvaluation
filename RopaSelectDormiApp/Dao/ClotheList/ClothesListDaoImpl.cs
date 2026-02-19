using Npgsql;
using RopaSelectDormiApp.Dto.ClotheList;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheList;
using static RopaSelectDormiApp.RowMapper.RowMapper;

namespace RopaSelectDormiApp.Dao.ClotheList;

public class ClothesListDaoImpl(NpgsqlDataSource dataSource): IClothesListDao
{
    public async Task AddClotheList(CreateClotheListDto createClotheList)
    {
        await using var cmd = dataSource.CreateCommand("INSERT INTO clothes_list (name, created_at) VALUES  (@name, @createdAt)");

        cmd.Parameters.AddWithValue("name", createClotheList.Name != null ? createClotheList.Name : DBNull.Value);
        cmd.Parameters.AddWithValue("createdAt", DateTime.Now);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<ClotheListModel>> FindAllClothesList()
    {
        await using var cmd = dataSource.CreateCommand("SELECT * FROM clothes_list");

        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheListModelRowMapper);
    }

    public async Task<List<ClotheListModel>> FindAllClothesListOrderedLimitOffset(long limit, long offset)
    {
        await using var cmd = dataSource.CreateCommand("SELECT * FROM clothes_list ORDER BY created_at DESC LIMIT @limit OFFSET @offset");

        cmd.Parameters.AddWithValue("limit", limit);
        cmd.Parameters.AddWithValue("offset", offset);

        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheListModelRowMapper);
    }

    public async Task<long> CountTotalAvailableLists()
    {
        await using var cmd = dataSource.CreateCommand("SELECT COUNT(id) FROM clothes_list");
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        return reader.GetInt64(0);
    }

    public async Task<List<ClotheModel>> FindClothesThatNotAreInListYet(long idClotheList)
    {
        const string sqlCommand = "SELECT clothes.id, name, description FROM clothes LEFT JOIN " +
                                  "(SELECT clothes_list_elements.* FROM clothes_list_elements WHERE id_clothes_list = @idClotheList) " +
                                  "AS vv ON vv.id_clothes = clothes.id WHERE id_clothes_list ISNULL ";
        await using var cmd = dataSource.CreateCommand(sqlCommand);
        cmd.Parameters.AddWithValue("idClotheList", idClotheList);
        await using var reader = await cmd.ExecuteReaderAsync();
        return await MapResultSetToList(reader, ClotheRowMapper);
    }

    public async Task DeleteClotheListById(long id)
    {
        await using var cmd = dataSource.CreateCommand("DELETE FROM clothes_list WHERE id = @id");
        cmd.Parameters.AddWithValue("id", id);
        await cmd.ExecuteNonQueryAsync();
    }
}