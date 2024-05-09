using APBD_pracadomowa6.DTO;

namespace APBD_pracadomowa6.Repository;

public interface IWarehouseRepository
{
    Task<bool> checkProductId(AddWarehouseProduct addWarehouseProduct);
    Task<bool> checkWarehouseId(AddWarehouseProduct addWarehouseProduct);
    Task<bool> checkAmount(AddWarehouseProduct addWarehouseProduct);
    Task<bool> checkIfInOrder(AddWarehouseProduct addWarehouseProduct);
    Task<int> addProductToWarehouseReturnId(AddWarehouseProduct addWarehouseProduct);
    Task updateFulfilledAt(AddWarehouseProduct addWarehouseProduct);
    // Task checkIfDone(AddWarehouseProduct addWarehouseProduct);
}