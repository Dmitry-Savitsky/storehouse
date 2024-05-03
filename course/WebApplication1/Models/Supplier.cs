using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore.MySQL.Models
{
    public class Supplier
    {
        [Key]
        [Column("idSupplier")]
        public int IdSupplier { get; set; }

        [Column("SupplierName")]
        public string SupplierName { get; set; }

        [Column("Address")]
        public string Address { get; set; }
    }
}