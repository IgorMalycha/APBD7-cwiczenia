using System.Data.SqlClient;
using APBD_pracadomowa6.DTO;

namespace APBD_pracadomowa6.Repository;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> checkIDsAndAmount(AddWarehouseProduct addWarehouseProduct)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT _ FROM Product WHERE IdProduct = @IdProduct";
        command.Parameters.AddWithValue("@IdProduct", addWarehouseProduct.IdProduct);
        
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();
        return res is not null;
    }
}