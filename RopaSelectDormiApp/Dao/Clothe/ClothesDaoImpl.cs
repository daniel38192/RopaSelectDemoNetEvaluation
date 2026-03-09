using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Model.Clothe;
using static RopaSelectDormiApp.RowMapper.RowMapper;

namespace RopaSelectDormiApp.Dao.Clothe;

public class ClothesDaoImpl(NpgsqlDataSource dataSource): IClothesDao
{
    public async Task<ClotheModel?> FindClotheById(long id, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "SELECT id, name, description FROM clothes WHERE id = @id AND user_id = @userId");

        cmd.Parameters.AddWithValue("id", id);
        cmd.Parameters.AddWithValue("userId", userId);
        
        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToObject(reader, ClotheRowMapper);
    }

    public async Task<List<ClotheModel>> FindAllClothes(Guid userId)
    {
        await using var cmd = dataSource.CreateCommand("SELECT id, name, description FROM clothes WHERE user_id = @userId");
        cmd.Parameters.AddWithValue("userId", userId);

        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheRowMapper);
    }

    public async Task AddClothe(CreateClotheDto addClothe, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand(
            "INSERT INTO clothes (user_id, name, description) VALUES (@userId, @name, @description)");

        cmd.Parameters.AddWithValue("userId", userId);
        cmd.Parameters.AddWithValue("name", addClothe.Name);
        cmd.Parameters.AddWithValue("description", addClothe.Description == null ? DBNull.Value : addClothe.Description!);
        
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteClotheById(long id, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand("DELETE FROM clothes WHERE id = @id AND user_id = @userId");
        
        cmd.Parameters.AddWithValue("id", id);
        cmd.Parameters.AddWithValue("userId", userId);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<bool> ExistClotheById(long id, Guid userId)
    {
        await using var cmd = dataSource.CreateCommand("SELECT EXISTS(SELECT * FROM clothes WHERE id = @id AND user_id = @userId)");
        cmd.Parameters.AddWithValue("id", id);
        cmd.Parameters.AddWithValue("userId", userId);

        await using var reader = await cmd.ExecuteReaderAsync();
        return await MapResultSetToObject(reader, dataReader => dataReader.GetBoolean(0));
    }
}
