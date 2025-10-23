using alamana.Application.Countries.DTOs;
using alamana.Application.ProductCountryPrices.DTOs;
using alamana.Application.ProductCountryPrices.Interfaces;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Application.WarehouseCategory.Interfaces;
using alamana.Contracts.ApiResponses.Error;
using alamana.Contracts.ApiResponses.Success;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alamana.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCountryPriceController : ControllerBase
    {
        private readonly IProductCountryPriceService _service;

        public ProductCountryPriceController(IProductCountryPriceService service) => _service = service;


        [HttpGet("GetProductCountryPriceById/{id:int}")]
        public async Task<IActionResult> GetProductCountryPriceById(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null)
                return this.NotFoundError("Product Country Price not found.", "سعر المنتج في البلد المحددة غير موجود.");

            return this.OkSuccess("Product Country Price fetched successfully", "تم جلب سعر المنتج في البلد المحددة بنجاح", dto);
        }





        [HttpPost("CreateProductCountryPrice")]
        public async Task<IActionResult> CreateProductCountryPrice([FromBody] CreateProductCountryPriceDto dto, CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            return this.OkSuccess("Product Country Price Added Successfully", "تم اضافه سعر المنتج في البلد بنجاح");
        }





        [HttpPut("UpdateProductCountryPrice/{id:int}")]
        public async Task<IActionResult> UpdateProductCountryPrice(int id, [FromBody] ProductCountryPriceDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return this.BadRequestError("Mismatched id.", "الرقم غير متطابق.");
            await _service.UpdateAsync(dto, ct);
            return this.OkSuccess("Product Country Price Updated Successfully", "تم تحديث سعر المنتج في البلد المحددة بنجاح");
        }




        [HttpDelete("DeleteProductCountryPrice/{id:int}")]
        public async Task<IActionResult> DeleteProductCountryPrice(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return this.OkSuccess("Product Country Price Deleted Successfully", "تم حذف سعر المنتج في البلد المحددة بنجاح");
        }
    }
}
