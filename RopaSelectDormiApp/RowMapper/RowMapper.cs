using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using RopaSelectDormiApp.Model.Clothe;
using RopaSelectDormiApp.Model.ClotheList;
using RopaSelectDormiApp.Model.ClotheListElement;

namespace RopaSelectDormiApp.RowMapper;

public static class RowMapper
{
    public static async Task<List<T>> MapResultSetToList<T>(NpgsqlDataReader reader, Func<NpgsqlDataReader, T> rowMapper)
    {
        List<T> resultSet = [];

        while (await reader.ReadAsync())
        {
            resultSet.Add(rowMapper(reader));
        }

        return resultSet;
    }

    public static async Task<T?> MapResultSetToObject<T>(NpgsqlDataReader reader, Func<NpgsqlDataReader, T> rowMapper)
    {
        var result = default(T);
        
        while (await reader.ReadAsync())
        {
            result = rowMapper(reader);
            break;
        }

        return result;
    }
    public static ClotheListModel ClotheListModelRowMapper(NpgsqlDataReader reader)
    {
        var id = reader.GetInt64(0);
        var name = reader.IsDBNull(1) ? null : reader.GetString(1);
        var createdAt = reader.GetDateTime(2);
        return new ClotheListModel(id, name, createdAt);
    }
    
    public static ClotheModel ClotheRowMapper(NpgsqlDataReader reader)
    {
        var id = reader.GetInt64(0);
        var name = reader.GetString(1);
        var description = reader["description"] == DBNull.Value ? null : (string?)reader["description"];
        return new ClotheModel(id, name, description);
    }
    
    
    public static ClotheListElementModel ClotheListElementModelRowMapper(NpgsqlDataReader reader)
    {
        var idClothes = reader.GetInt64(0);
        var quantity = reader.GetInt64(1);
        return new ClotheListElementModel(idClothes, quantity);
    }

    public static ClotheListElementIdNameQuantityModel ClotheListElementIdNameQuantityModelRowMapper(
        NpgsqlDataReader reader)
    {
        return new ClotheListElementIdNameQuantityModel(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetInt64(2)
        );
    }
}