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

    public class BuyerController : ControllerBase
    {
        private readonly RepairManagementDbContext _repairManagementDbContext;

        public BuyerController(RepairManagementDbContext repairManagementDbContext)
        {
            _repairManagementDbContext = repairManagementDbContext;
        }

        [HttpPost("addBuyer")]
        public async Task<IActionResult> AddBuyer(Buyer buyer)
        {
            _repairManagementDbContext.Buyers.Add(buyer);
            await _repairManagementDbContext.SaveChangesAsync();

            return Ok(buyer);
        }

        [HttpGet("getAllBuyers")]
        public async Task<IActionResult> GetAllBuyers()
        {
            var buyers = await _repairManagementDbContext.Buyers.ToListAsync();

            return Ok(buyers);
        }

        [HttpDelete("deleteBuyer/{id}")]
        public async Task<IActionResult> DeleteBuyer(int id)
        {
            var buyer = await _repairManagementDbContext.Buyers.FindAsync(id);
            if (buyer != null)
            {
                _repairManagementDbContext.Buyers.Remove(buyer);
                await _repairManagementDbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("updateBuyer/{id}")]
        public async Task<IActionResult> UpdateBuyer(int id, Buyer updatedBuyer)
        {
            var buyer = await _repairManagementDbContext.Buyers.FindAsync(id);
            if (buyer != null)
            {
                buyer.BuyerName = updatedBuyer.BuyerName;
                buyer.BuyerPhone = updatedBuyer.BuyerPhone;

                await _repairManagementDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getBuyerById/{id}")]
        public async Task<IActionResult> GetBuyerById(int id)
        {
            var buyer = await _repairManagementDbContext.Buyers.FindAsync(id);
            if (buyer != null)
            {
                return Ok(buyer);
            }

            return NotFound();
        }

        [HttpGet("searchBuyers/{keyword}")]
        public async Task<IActionResult> SearchBuyers(string keyword)
        {
            var buyers = await _repairManagementDbContext.Buyers
                .Where(b => b.BuyerName.Contains(keyword) || b.BuyerPhone.Contains(keyword))
                .ToListAsync();

            return Ok(buyers);
        }

    }
}

