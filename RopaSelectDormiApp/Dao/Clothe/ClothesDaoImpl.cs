using Npgsql;
using RopaSelectDormiApp.Dto.Clothe;
using RopaSelectDormiApp.Model.Clothe;
using static RopaSelectDormiApp.RowMapper.RowMapper;

namespace RopaSelectDormiApp.Dao.Clothe;

public class ClothesDaoImpl(NpgsqlDataSource dataSource): IClothesDao
{
    public async Task<ClotheModel?> FindClotheById(long id)
    {
        await using var cmd = dataSource.CreateCommand("SELECT id, name, description FROM clothes where id = @id");

        cmd.Parameters.AddWithValue("id", id);
        
        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToObject(reader, ClotheRowMapper);
    }

    public async Task<List<ClotheModel>> FindAllClothes()
    {
        await using var cmd = dataSource.CreateCommand("SELECT id, name, description FROM clothes");

        await using var reader = await cmd.ExecuteReaderAsync();

        return await MapResultSetToList(reader, ClotheRowMapper);
    }

    public async Task AddClothe(CreateClotheDto addClothe)
    {
        await using var cmd = dataSource.CreateCommand("INSERT INTO clothes (name, description) VALUES (@name, @description)");

        cmd.Parameters.AddWithValue("name", addClothe.Name);
        cmd.Parameters.AddWithValue("description", addClothe.Description == null ? DBNull.Value : addClothe.Description!);
        
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteClotheById(long id)
    {
        await using var cmd = dataSource.CreateCommand("DELETE FROM clothes WHERE id = @id");
        
        cmd.Parameters.AddWithValue("id", id);

        await cmd.ExecuteNonQueryAsync();
    }
    
}