using System.ComponentModel.DataAnnotations;

namespace APBD_pracadomowa6.DTO;

public class AddWarehouseProduct
{
    [Required]
    public int IdProduct{ get; set; }
    [Required]
    public int Type { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public string CreatedAt { get; set; }
}