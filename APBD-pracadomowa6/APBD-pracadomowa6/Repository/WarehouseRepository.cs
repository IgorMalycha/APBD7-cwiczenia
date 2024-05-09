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

    public async Task<bool> checkProductId(AddWarehouseProduct addWarehouseProduct)
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

    public async Task<bool> checkWarehouseId(AddWarehouseProduct addWarehouseProduct)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT _ FROM Product WHERE IdWarehouse = @IdWarehouse";
        command.Parameters.AddWithValue("@IdWarehouse", addWarehouseProduct.IdWarehouse);
        
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();
        return res is not null;
    }
    public async Task<bool> checkAmount(AddWarehouseProduct addWarehouseProduct)
    {
        if (addWarehouseProduct.Amount > 0)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> checkIfInOrder(AddWarehouseProduct addWarehouseProduct)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT _ FROM Order WHERE IdProduct = @IdProduct";
        command.Parameters.AddWithValue("@IdProduct", addWarehouseProduct.IdProduct);
        
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();
        if (res == null)
        {
            return false;
        }
        
        
        using SqlConnection connection2 = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command2 = new SqlCommand();
        command2.Connection = connection;
        command2.CommandText = "SELECT Amount FROM Order WHERE IdProduct = @IdProduct";
        command2.Parameters.AddWithValue("@IdProduct", addWarehouseProduct.IdProduct);
        
        await connection2.OpenAsync();

        var reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        if (!reader.HasRows) throw new Exception();

        var orderAmount = reader.GetOrdinal("Amount");
        int amount = reader.GetInt32(orderAmount);

        if (amount != addWarehouseProduct.Amount)
        {
            return false;
        }
        return true;
    }

    public async Task updateFulfilledAt(AddWarehouseProduct addWarehouseProduct)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Order SET FulfilledAt = GETDATE() WHERE IdProduct = @IdProduct";
        command.Parameters.AddWithValue("@IdProduct", addWarehouseProduct.CreatedAt);
        
        await connection.OpenAsync();

        await command.ExecuteScalarAsync();
        
    }
    public async Task<int> addProductToWarehouseReturnId(AddWarehouseProduct addWarehouseProduct)
    {
        using SqlConnection connection2 = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command2 = new SqlCommand();
        command2.Connection = connection2;
        command2.CommandText = "SELECT IdOrder FROM Order WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt = @CreatedAt";
        command2.Parameters.AddWithValue("@IdProduct", addWarehouseProduct.IdProduct);
        command2.Parameters.AddWithValue("@Amount", addWarehouseProduct.Amount);
        command2.Parameters.AddWithValue("@CreatedAt", addWarehouseProduct.CreatedAt);
        
        await connection2.OpenAsync();

        var reader2 = await command2.ExecuteReaderAsync();

        await reader2.ReadAsync();

        if (!reader2.HasRows) throw new Exception();

        var IdOrder = reader2.GetOrdinal("IdOrder");
        int IdOrderRes = reader2.GetInt32(IdOrder);
        
        
        
        using SqlConnection connection3 = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command3 = new SqlCommand();
        command3.Connection = connection3;
        command3.CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
        command3.Parameters.AddWithValue("@IdProduct", addWarehouseProduct.IdProduct);
        
        
        await connection3.OpenAsync();

        var reader3 = await command3.ExecuteReaderAsync();

        await reader3.ReadAsync();

        if (!reader3.HasRows) throw new Exception();

        var Price = reader3.GetOrdinal("Price");
        int PriceRes = reader3.GetInt32(Price);

        int PriceFinal = PriceRes * addWarehouseProduct.Amount;
        
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, GETDATE())";
        command.Parameters.AddWithValue("@IdProduct", addWarehouseProduct.IdProduct);
        command.Parameters.AddWithValue("@IdWarehouse", addWarehouseProduct.IdWarehouse);
        command.Parameters.AddWithValue("@IdOrder", IdOrderRes);
        command.Parameters.AddWithValue("@Amount", addWarehouseProduct.Amount);
        command.Parameters.AddWithValue("@Price", PriceFinal);
        command.Parameters.AddWithValue("@CreatedAt", addWarehouseProduct.CreatedAt);
        
        await connection.OpenAsync();
        
        await command.ExecuteScalarAsync();
        
        
        using SqlConnection connection4 = new SqlConnection(_configuration.GetConnectionString("Default"));

        using SqlCommand command4 = new SqlCommand();
        command4.Connection = connection3;
        command4.CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
        command4.Parameters.AddWithValue("@IdProduct", addWarehouseProduct.IdProduct);
        
        
        await connection4.OpenAsync();

        var reader4 = await command4.ExecuteReaderAsync();

        await reader4.ReadAsync();

        if (!reader4.HasRows) throw new Exception();

        var IdProductWarehouse = reader3.GetOrdinal("IdProductWarehouse");
        int Id = reader3.GetInt32(IdProductWarehouse);

        return Id;
    }
    
    
    
}