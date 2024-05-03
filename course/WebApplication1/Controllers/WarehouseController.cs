using EntityFrameworkCore.MySQL.Data;
using EntityFrameworkCore.MySQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly RepairManagementDbContext _repairManagementDbContext;

        public WarehouseController(RepairManagementDbContext repairManagementDbContext)
        {
            _repairManagementDbContext = repairManagementDbContext;
        }

        //

        [HttpPost("addWarehouse")]
        public async Task<IActionResult> AddWarehouse(Warehouse warehouse)
        {
            _repairManagementDbContext.Warehouses.Add(warehouse);
            await _repairManagementDbContext.SaveChangesAsync();

            return Ok(warehouse);
        }

        [HttpGet("getAllWarehouses")]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _repairManagementDbContext.Warehouses.ToListAsync();

            return Ok(warehouses);
        }

        [HttpDelete("deleteWarehouse/{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var warehouse = await _repairManagementDbContext.Warehouses.FindAsync(id);
            if (warehouse != null)
            {
                _repairManagementDbContext.Warehouses.Remove(warehouse);
                await _repairManagementDbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("updateWarehouse/{id}")]
        public async Task<IActionResult> UpdateWarehouse(int id, Warehouse updatedWarehouse)
        {
            var warehouse = await _repairManagementDbContext.Warehouses.FindAsync(id);
            if (warehouse != null)
            {
                warehouse.WarehouseName = updatedWarehouse.WarehouseName;
                warehouse.WarehouseAddress = updatedWarehouse.WarehouseAddress;
                warehouse.WarehousePhone = updatedWarehouse.WarehousePhone;

                await _repairManagementDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getWarehouse/{id}")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var warehouse = await _repairManagementDbContext.Warehouses.FindAsync(id);

            if (warehouse != null)
            {
                return Ok(warehouse);
            }

            return NotFound();
        }

           //

        [HttpGet("searchWarehouses/{keyword}")]
        public async Task<IActionResult> SearchWarehouses(string keyword)
        {
            var warehouses = await _repairManagementDbContext.Warehouses
                .Where(w =>
                    EF.Functions.Like(w.WarehouseName, $"%{keyword}%") ||
                    EF.Functions.Like(w.WarehouseAddress, $"%{keyword}%") ||
                    EF.Functions.Like(w.WarehousePhone, $"%{keyword}%"))
                .ToListAsync();

            if (warehouses.Any())
            {
                return Ok(warehouses);
            }

            return NotFound();
        }


    }


}
