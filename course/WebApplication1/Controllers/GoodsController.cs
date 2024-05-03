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
    public class GoodsController : ControllerBase
    {
        private readonly RepairManagementDbContext _repairManagementDbContext;

        public GoodsController(RepairManagementDbContext repairManagementDbContext)
        {
            _repairManagementDbContext = repairManagementDbContext;
        }

        [HttpPost("addGoods")]
        public async Task<IActionResult> AddGoods(Goods goods)
        {
            _repairManagementDbContext.Goods.Add(goods);
            await _repairManagementDbContext.SaveChangesAsync();

            return Ok(goods);
        }

        [HttpGet("getAllGoods")]
        public async Task<IActionResult> GetAllGoods()
        {
            var goodsList = await _repairManagementDbContext.Goods.Include(g => g.Manufacturer).ToListAsync();

            return Ok(goodsList);
        }

        [HttpDelete("deleteGoods/{id}")]
        public async Task<IActionResult> DeleteGoods(int id)
        {
            var goods = await _repairManagementDbContext.Goods.FindAsync(id);
            if (goods != null)
            {
                _repairManagementDbContext.Goods.Remove(goods);
                await _repairManagementDbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("updateGoods/{id}")]
        public async Task<IActionResult> UpdateGoods(int id, Goods updatedGoods)
        {
            var goods = await _repairManagementDbContext.Goods.FindAsync(id);
            if (goods != null)
            {
                goods.GoodsName = updatedGoods.GoodsName;
                goods.GoodsType = updatedGoods.GoodsType;
                goods.GoodsPrice = updatedGoods.GoodsPrice;
                goods.IdManufacturer = updatedGoods.IdManufacturer;

                await _repairManagementDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getGoodsByManufacturer/{manufacturerId}")]
        public async Task<IActionResult> GetGoodsByManufacturer(int manufacturerId)
        {
            var goodsList = await _repairManagementDbContext.Goods
                .Where(g => g.IdManufacturer == manufacturerId)
                .ToListAsync();

            return Ok(goodsList);
        }

        [HttpGet("getGoodsWithManufacturer")]
        public async Task<IActionResult> GetGoodsWithManufacturer()
        {
            var goodsList = await _repairManagementDbContext.Goods
                .Include(g => g.Manufacturer)
                .ToListAsync();

            return Ok(goodsList);
        }
    }

}
