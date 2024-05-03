using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace EntityFrameworkCore.MySQL.Models
{
    public class Goods
    {
        [Key]
        [Column("idGoods")]
        public int IdGoods { get; set; }

        [Column("GoodsName")]
        public string GoodsName { get; set; }

        [Column("GoodsType")]
        public int? GoodsType { get; set; }

        [Column("GoodsPrice")]
        public int? GoodsPrice { get; set; }

        [ForeignKey("Manufacturer")]
        [Column("idManufacturer")]
        public int IdManufacturer { get; set; }

        public Manufacturer? Manufacturer { get; set; }
    }
}