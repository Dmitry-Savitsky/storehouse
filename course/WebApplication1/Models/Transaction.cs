using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace EntityFrameworkCore.MySQL.Models
{
    public class Transaction
    {
        [Key]
        [Column("idTransaction")]
        public int IdTransaction { get; set; }

        [Column("TransactionDate")]
        public DateTime? TransactionDate { get; set; }

        [ForeignKey("Goods")]
        [Column("idGoods")]
        public int IdGoods { get; set; }

        [Column("TransactionCount")]
        public int? TransactionCount { get; set; }

        [ForeignKey("Supplier")]
        [Column("idSupplier")]
        public int IdSupplier { get; set; }

        [ForeignKey("WarehouseSender")]
        [Column("idWarehouseSender")]
        public int IdWarehouseSender { get; set; }

        [ForeignKey("WarehouseReceiver")]
        [Column("idWarehouseReceiver")]
        public int IdWarehouseReceiver { get; set; }

        [ForeignKey("Buyer")]
        [Column("idBuyer")]
        public int IdBuyer { get; set; }

        public Goods? Goods { get; set; }
        public Supplier? Supplier { get; set; }
        public Warehouse? WarehouseSender { get; set; }
        public Warehouse? WarehouseReceiver { get; set; }
        public Buyer? Buyer { get; set; }
    }
}
