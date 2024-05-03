using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore.MySQL.Models
{
    public class Storage
    {
        [Key]
        [Column("idStorage")]
        public int IdStorage { get; set; }

        [ForeignKey("Warehouse")]
        [Column("idWarehouse")]
        public int IdWarehouse { get; set; }

        [ForeignKey("Goods")]
        [Column("idGoods")]
        public int IdGoods { get; set; }

        [Column("StorageCount")]
        public int? StorageCount { get; set; }

        public Warehouse? Warehouse { get; set; }
        public Goods? Goods { get; set; }
    }
}   