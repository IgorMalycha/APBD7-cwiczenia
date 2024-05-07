using APBD_pracadomowa6.DTO;

namespace APBD_pracadomowa6.Repository;

public interface IWarehouseRepository
{
    Task<bool> checkIDsAndAmount(AddWarehouseProduct addWarehouseProduct);
}