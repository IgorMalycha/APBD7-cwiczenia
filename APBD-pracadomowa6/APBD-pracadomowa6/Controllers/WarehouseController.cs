using APBD_pracadomowa6.DTO;
using APBD_pracadomowa6.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD_pracadomowa6.Controllers;


[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private IWarehouseRepository _warehouseRepository;

    public WarehouseController(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddWarehouseProduct warehouseProduct)
    {
        if (!await _warehouseRepository.checkProductId(warehouseProduct))
        {
            return NotFound();
        }
        if (!await _warehouseRepository.checkWarehouseId(warehouseProduct))
        {
            return NotFound();
        }
        if (!await _warehouseRepository.checkAmount(warehouseProduct))
        {
            return NotFound();
        }
        if (!await _warehouseRepository.checkIfInOrder(warehouseProduct))
        {
            return NotFound();
        }
        _warehouseRepository.updateFulfilledAt(warehouseProduct);
        var id = _warehouseRepository.addProductToWarehouseReturnId(warehouseProduct);

        return Created("api/warehouse", id);
    }
}