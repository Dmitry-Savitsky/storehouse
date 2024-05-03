using EntityFrameworkCore.MySQL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Buyer
    {
        [Key]
        [Column("idBuyer")]
        public int IdBuyer { get; set; }

        [Column("BuyerName")]
        public string BuyerName { get; set; }

        [Column("BuyerPhone")]
        public string BuyerPhone { get; set; }
    }
}
