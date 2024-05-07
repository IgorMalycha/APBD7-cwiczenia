using APBD_pracadomowa6.DTO;
using APBD_pracadomowa6.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APBD_pracadomowa6.Controllers;


[Route("[controller]")]
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
        if (!await _warehouseRepository.checkIDsAndAmount(warehouseProduct))
        {
            return NotFound();
        }
        
    }
}