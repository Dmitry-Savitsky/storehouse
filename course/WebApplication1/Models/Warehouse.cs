using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace EntityFrameworkCore.MySQL.Models
{
    public class Warehouse
    {
        [Key]
        [Column("idWarehouse")]
        public int IdWarehouse { get; set; }

        [Column("WarehouseName")]
        public string WarehouseName { get; set; }

        [Column("WarehouseAddress")]
        public string WarehouseAddress { get; set; }

        [Column("WarehousePhone")]
        public string WarehousePhone { get; set; }
    }
}