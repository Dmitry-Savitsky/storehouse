using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EntityFrameworkCore.MySQL.Models;
using EntityFrameworkCore.MySQL.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly RepairManagementDbContext _repairManagementDbContext;

        public SupplierController(RepairManagementDbContext repairManagementDbContext)
        {
            _repairManagementDbContext = repairManagementDbContext;
        }

        [HttpPost("addCustomer")]
        public async Task<IActionResult> AddCustomer(Supplier customer)
        {
            _repairManagementDbContext.Suppliers.Add(customer);
            await _repairManagementDbContext.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpGet("getAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _repairManagementDbContext.Suppliers.ToListAsync();

            return Ok(customers);
        }

        [HttpDelete("deleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _repairManagementDbContext.Suppliers.FindAsync(id);
            if (customer != null)
            {
                _repairManagementDbContext.Suppliers.Remove(customer);
                await _repairManagementDbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("updateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Supplier updatedCustomer)
        {
            var customer = await _repairManagementDbContext.Suppliers.FindAsync(id);
            if (customer != null)
            {
                customer.SupplierName = updatedCustomer.SupplierName;
                customer.Address = updatedCustomer.Address;

                await _repairManagementDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getSupplierById/{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var supplier = await _repairManagementDbContext.Suppliers.FindAsync(id);

            if (supplier != null)
            {
                return Ok(supplier);
            }

            return NotFound();
        }

        [HttpGet("searchSuppliersByKey/{keyword}")]
        public async Task<IActionResult> SearchSuppliersByKey(string keyword)
        {
            var suppliers = await _repairManagementDbContext.Suppliers
                .Where(s => s.SupplierName.Contains(keyword) || s.Address.Contains(keyword))
                .ToListAsync();

            return Ok(suppliers);
        }

    }

}
