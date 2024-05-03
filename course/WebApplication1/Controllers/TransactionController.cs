using EntityFrameworkCore.MySQL.Data;
using EntityFrameworkCore.MySQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly RepairManagementDbContext _repairManagementDbContext;

        public TransactionController(RepairManagementDbContext repairManagementDbContext)
        {
            _repairManagementDbContext = repairManagementDbContext;
        }

        [HttpPost("addTransaction")]
        public async Task<IActionResult> AddTransaction(Transaction transaction)
        {
            _repairManagementDbContext.Transactions.Add(transaction);
            await _repairManagementDbContext.SaveChangesAsync();

            return Ok(transaction);
        }

        [HttpGet("getAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _repairManagementDbContext.Transactions
                .Include(t => t.Goods)
                .Include(t => t.Supplier)
                .Include(t => t.WarehouseSender)
                .Include(t => t.WarehouseReceiver)
                .Include(t => t.Buyer)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpDelete("deleteTransaction/{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _repairManagementDbContext.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _repairManagementDbContext.Transactions.Remove(transaction);
                await _repairManagementDbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("updateTransaction/{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, Transaction updatedTransaction)
        {
            var transaction = await _repairManagementDbContext.Transactions.FindAsync(id);
            if (transaction != null)
            {
                transaction.TransactionDate = updatedTransaction.TransactionDate;
                transaction.IdGoods = updatedTransaction.IdGoods;
                transaction.TransactionCount = updatedTransaction.TransactionCount;
                transaction.IdSupplier = updatedTransaction.IdSupplier;
                transaction.IdWarehouseSender = updatedTransaction.IdWarehouseSender;
                transaction.IdWarehouseReceiver = updatedTransaction.IdWarehouseReceiver;
                transaction.IdBuyer = updatedTransaction.IdBuyer;

                await _repairManagementDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getTransactionsByBuyer/{buyerId}")]
        public async Task<IActionResult> GetTransactionsByBuyer(int buyerId)
        {
            var transactions = await _repairManagementDbContext.Transactions
                .Include(t => t.Goods)
                .Include(t => t.Supplier)
                .Include(t => t.WarehouseSender)
                .Include(t => t.WarehouseReceiver)
                .Include(t => t.Buyer)
                .Where(t => t.IdBuyer == buyerId)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("getTransactionsByGoodsType/{goodsType}")]
        public async Task<IActionResult> GetTransactionsByGoodsType(int goodsType)
        {
            var transactions = await _repairManagementDbContext.Transactions
                .Include(t => t.Goods)
                .Include(t => t.Supplier)
                .Include(t => t.WarehouseSender)
                .Include(t => t.WarehouseReceiver)
                .Include(t => t.Buyer)
                .Where(t => t.Goods != null && t.Goods.GoodsType == goodsType)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("getTransactionsWithStorageDetails")]
        public async Task<IActionResult> GetTransactionsWithStorageDetails()
        {
            var transactions = await _repairManagementDbContext.Transactions
                .Include(t => t.Goods)
                .Include(t => t.Supplier)
                .Include(t => t.WarehouseSender)
                .Include(t => t.WarehouseReceiver)
                .Include(t => t.Buyer)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("getTransactionsByDateRange")]
        public async Task<IActionResult> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            var transactions = await _repairManagementDbContext.Transactions
                .Include(t => t.Goods)
                .Include(t => t.Supplier)
                .Include(t => t.WarehouseSender)
                .Include(t => t.WarehouseReceiver)
                .Include(t => t.Buyer)
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("getRecentTransactions")]
        public async Task<IActionResult> GetRecentTransactions(int numberOfTransactions = 10)
        {
            var recentTransactions = await _repairManagementDbContext.Transactions
                .OrderByDescending(t => t.TransactionDate)
                .Take(numberOfTransactions)
                .Include(t => t.Goods)
                .Include(t => t.Supplier)
                .Include(t => t.WarehouseSender)
                .Include(t => t.WarehouseReceiver)
                .Include(t => t.Buyer)
                .ToListAsync();

            return Ok(recentTransactions);
        }

        [HttpGet("getGoodsWithLowStock")]
        public async Task<IActionResult> GetGoodsWithLowStock(int threshold = 20)
        {
            var goodsWithLowStock = await _repairManagementDbContext.Storages
                .Where(s => s.StorageCount < threshold)
                .Include(s => s.Goods)
                .Select(s => new
                {
                    Goods = s.Goods,
                    CurrentStock = s.StorageCount
                })
                .ToListAsync();

            return Ok(goodsWithLowStock);
        }

        [HttpGet("getSuppliersWithMostTransactions")]
        public async Task<IActionResult> GetSuppliersWithMostTransactions(int topCount = 3)
        {
            var topSuppliers = await _repairManagementDbContext.Suppliers
                .Join(
                    _repairManagementDbContext.Transactions,
                    supplier => supplier.IdSupplier,
                    transaction => transaction.IdSupplier,
                    (supplier, transactions) => new { Supplier = supplier, Transaction = transactions }
                )
                .GroupBy(s => s.Supplier)
                .Select(group => new
                {
                    Supplier = group.Key,
                    TransactionCount = group.Count()
                })
                .OrderByDescending(s => s.TransactionCount)
                .Take(topCount)
                .ToListAsync();

            return Ok(topSuppliers);
        }

        [HttpGet("getWarehouseTransactionSummary")]
        public async Task<IActionResult> GetWarehouseTransactionSummary(int warehouseId)
        {
            var warehouseTransactionSummary = await _repairManagementDbContext.Transactions
                .Where(t => t.IdWarehouseSender == warehouseId || t.IdWarehouseReceiver == warehouseId)
                .GroupBy(t => new
                {
                    Year = t.TransactionDate.HasValue ? t.TransactionDate.Value.Year : (int?)null,
                    Month = t.TransactionDate.HasValue ? t.TransactionDate.Value.Month : (int?)null
                })
                .Select(group => new
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    TotalTransactions = group.Count(),
                    TotalTransactionCount = group.Sum(t => t.TransactionCount ?? 0)
                })
                .OrderByDescending(summary => summary.Year)
                .ThenByDescending(summary => summary.Month)
                .ToListAsync();

            return Ok(warehouseTransactionSummary);
        }

    }

}
