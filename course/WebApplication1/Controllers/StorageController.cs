using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using EntityFrameworkCore.MySQL.Models;
using EntityFrameworkCore.MySQL.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class StorageController : ControllerBase
    {
        private readonly RepairManagementDbContext _repairManagementDbContext;

        public StorageController(RepairManagementDbContext repairManagementDbContext)
        {
            _repairManagementDbContext = repairManagementDbContext;
        }

        [HttpPost("addStorage")]
        public async Task<IActionResult> AddStorage(Storage storage)
        {
            _repairManagementDbContext.Storages.Add(storage);
            await _repairManagementDbContext.SaveChangesAsync();

            return Ok(storage);
        }

        [HttpGet("getAllStorages")]
        public async Task<IActionResult> GetAllStorages()
        {
            var storages = await _repairManagementDbContext.Storages.ToListAsync();

            return Ok(storages);
        }

        [HttpDelete("deleteStorage/{id}")]
        public async Task<IActionResult> DeleteStorage(int id)
        {
            var storage = await _repairManagementDbContext.Storages.FindAsync(id);
            if (storage != null)
            {
                _repairManagementDbContext.Storages.Remove(storage);
                await _repairManagementDbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("updateStorage/{id}")]
        public async Task<IActionResult> UpdateStorage(int id, Storage updatedStorage)
        {
            var storage = await _repairManagementDbContext.Storages.FindAsync(id);
            if (storage != null)
            {
                storage.IdWarehouse = updatedStorage.IdWarehouse;
                storage.IdGoods = updatedStorage.IdGoods;
                storage.StorageCount = updatedStorage.StorageCount;

                await _repairManagementDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getGoodsByWarehouse/{warehouseId}")]
        public async Task<IActionResult> GetGoodsByWarehouse(int warehouseId)
        {
            var goodsByWarehouse = await _repairManagementDbContext.Storages
                .Where(s => s.IdWarehouse == warehouseId)
                .Include(s => s.Goods)
                .Select(s => s.Goods)
                .ToListAsync();

            return Ok(goodsByWarehouse);
        }

        [HttpGet("getWarehouseByGoods/{goodsId}")]
        public async Task<IActionResult> GetWarehouseByGoods(int goodsId)
        {
            var warehouseByGoods = await _repairManagementDbContext.Storages
                .Where(s => s.IdGoods == goodsId)
                .Include(s => s.Warehouse)
                .Select(s => s.Warehouse)
                .ToListAsync();

            return Ok(warehouseByGoods);
        }

        [HttpGet("getStorageDetails/{id}")]
        public async Task<IActionResult> GetStorageDetails(int id)
        {
            var storageDetails = await _repairManagementDbContext.Storages
                .Include(s => s.Warehouse)
                .Include(s => s.Goods)
                .FirstOrDefaultAsync(s => s.IdStorage == id);

            if (storageDetails != null)
            {
                return Ok(storageDetails);
            }

            return NotFound();
        }

    }

}
