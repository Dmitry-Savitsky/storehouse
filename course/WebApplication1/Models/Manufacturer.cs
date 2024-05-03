using EntityFrameworkCore.MySQL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Manufacturer
    {
        [Key]
        [Column("idManufacturer")]
        public int IdManufacturer { get; set; }

        [Column("ManufacturerName")]
        public string ManufacturerName { get; set; }

        [Column("Manufactureaddress")]
        public string ManufactureAddress { get; set; }
    }
}
