using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EntityFrameworkCore.MySQL.Models;
using EntityFrameworkCore.MySQL.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ManufacturerController : ControllerBase
    {
        private readonly RepairManagementDbContext _repairManagementDbContext;

        public ManufacturerController(RepairManagementDbContext repairManagementDbContext)
        {
            _repairManagementDbContext = repairManagementDbContext;
        }

        [HttpPost("addManufacturer")]
        public async Task<IActionResult> AddManufacturer(Manufacturer manufacturer)
        {
            _repairManagementDbContext.Manufacturers.Add(manufacturer);
            await _repairManagementDbContext.SaveChangesAsync();

            return Ok(manufacturer);
        }

        [HttpGet("getAllManufacturers")]
        public async Task<IActionResult> GetAllManufacturers()
        {
            var manufacturers = await _repairManagementDbContext.Manufacturers.ToListAsync();

            return Ok(manufacturers);
        }

        [HttpDelete("deleteManufacturer/{id}")]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            var manufacturer = await _repairManagementDbContext.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                _repairManagementDbContext.Manufacturers.Remove(manufacturer);
                await _repairManagementDbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("updateManufacturer/{id}")]
        public async Task<IActionResult> UpdateManufacturer(int id, Manufacturer updatedManufacturer)
        {
            var manufacturer = await _repairManagementDbContext.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                manufacturer.ManufacturerName = updatedManufacturer.ManufacturerName;
                manufacturer.ManufactureAddress = updatedManufacturer.ManufactureAddress;

                await _repairManagementDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getManufacturer/{id}")]
        public async Task<IActionResult> GetManufacturerById(int id)
        {
            var manufacturer = await _repairManagementDbContext.Manufacturers.FindAsync(id);

            if (manufacturer != null)
            {
                return Ok(manufacturer);
            }

            return NotFound();
        }

        [HttpGet("searchManufacturers/{keyword}")]
        public async Task<IActionResult> SearchManufacturers(string keyword)
        {
            var manufacturers = await _repairManagementDbContext.Manufacturers
                .Where(m => m.ManufacturerName.Contains(keyword) || m.ManufactureAddress.Contains(keyword))
                .ToListAsync();

            return Ok(manufacturers);
        }

    }

}
