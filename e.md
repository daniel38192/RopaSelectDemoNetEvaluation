```csharp
using Npgsql;

namespace RopaSelectDormiApp.Config;

public class DataSourceConfigurerImpl: IDataSourceConfigurer
{
    private readonly NpgsqlDataSource _dataSource;

    public DataSourceConfigurerImpl()
    {
        const string connectionString = "Host=localhost;Port=6432;Username=postgres;Password=postgres;Database=ropaDormiSelectDB"; 
        _dataSource = NpgsqlDataSource.Create(connectionString);
    }

    public NpgsqlDataSource GetDatasource() => _dataSource;
}
```