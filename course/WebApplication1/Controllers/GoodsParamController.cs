using EntityFrameworkCore.MySQL.Data;
using EntityFrameworkCore.MySQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsParamController : ControllerBase
    {
        private readonly RepairManagementDbContext _repairManagementDbContext;

        public GoodsParamController(RepairManagementDbContext repairManagementDbContext)
        {
            _repairManagementDbContext = repairManagementDbContext;
        }

        [HttpPost("addGoodsParam")]
        public async Task<IActionResult> AddGoodsParam(GoodsParam goodsParam)
        {
            _repairManagementDbContext.GoodsParams.Add(goodsParam);
            await _repairManagementDbContext.SaveChangesAsync();

            return Ok(goodsParam);
        }

        [HttpGet("getAllGoodsParams")]
        public async Task<IActionResult> GetAllGoodsParams()
        {
            var goodsParams = await _repairManagementDbContext.GoodsParams.ToListAsync();

            return Ok(goodsParams);
        }

        [HttpDelete("deleteGoodsParam/{id}")]
        public async Task<IActionResult> DeleteGoodsParam(int id)
        {
            var goodsParam = await _repairManagementDbContext.GoodsParams.FindAsync(id);
            if (goodsParam != null)
            {
                _repairManagementDbContext.GoodsParams.Remove(goodsParam);
                await _repairManagementDbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("updateGoodsParam/{id}")]
        public async Task<IActionResult> UpdateGoodsParam(int id, GoodsParam updatedGoodsParam)
        {
            var goodsParam = await _repairManagementDbContext.GoodsParams.FindAsync(id);
            if (goodsParam != null)
            {
                goodsParam.GoodsParamName = updatedGoodsParam.GoodsParamName;
                goodsParam.GoodsParamValue = updatedGoodsParam.GoodsParamValue;
                goodsParam.GoodsParamMeasure = updatedGoodsParam.GoodsParamMeasure;

                await _repairManagementDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("getGoodsParamsByGoodsId/{goodsId}")]
        public async Task<IActionResult> GetGoodsParamsByGoodsId(int goodsId)
        {
            var goodsParams = await _repairManagementDbContext.GoodsParams
                .Where(gp => gp.IdGoods == goodsId)
                .ToListAsync();

            return Ok(goodsParams);
        }

        [HttpGet("getGoodsParamDetails/{goodsParamId}")]
        public async Task<IActionResult> GetGoodsParamDetails(int goodsParamId)
        {
            var goodsParamDetails = await _repairManagementDbContext.GoodsParams
                .Include(gp => gp.Goods)
                .Where(gp => gp.IdGoodsParam == goodsParamId)
                .FirstOrDefaultAsync();

            return Ok(goodsParamDetails);
        }
    }

}
