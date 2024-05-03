using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace EntityFrameworkCore.MySQL.Models
{
    public class GoodsParam
    {
        [Key]
        [Column("idGoodsParam")]
        public int IdGoodsParam { get; set; }

        [ForeignKey("Goods")]
        [Column("idGoods")]
        public int IdGoods { get; set; }

        [Column("GoodsParamName")]
        public string GoodsParamName { get; set; }

        [Column("GoodsParamValue")]
        public string GoodsParamValue { get; set; }

        [Column("GoodsParamMeasure")]
        public string GoodsParamMeasure { get; set; }

        public Goods? Goods { get; set; }
    }
}
