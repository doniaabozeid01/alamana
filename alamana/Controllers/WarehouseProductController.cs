using System.Collections.Generic;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Application.WarehouseCategory.Interfaces;
using alamana.Application.WarehouseProduct.DTOs;
using alamana.Application.WarehouseProduct.Interfaces;
using alamana.Contracts.ApiResponses.Error;
using alamana.Contracts.ApiResponses.Success;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alamana.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseProductController : ControllerBase
    {
        private readonly IWarehouseProductService _service;

        public WarehouseProductController(IWarehouseProductService service) => _service = service;


        [HttpGet("GetWarehouseProductById/{id:int}")]
        public async Task<IActionResult> GetWarehouseProductById(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null)
                return this.NotFoundError("Warehouse Product not found.", "المنتج في المخزن غير موجود.");

            return this.OkSuccess("Warehouse Product fetched successfully", "تم جلب المنتج في المخزن بنجاح", dto);
        }





        [HttpPost("CreateWarehouseProduct")]
        public async Task<IActionResult> CreateWarehouseProduct([FromBody] CreateWarehouseProductDto dto, CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            return this.OkSuccess("Warehouse Product Added Successfully", "تم اضافه المنتج في المخزن بنجاح");
        }





        [HttpPut("UpdateWarehouseProduct/{id:int}")]
        public async Task<IActionResult> UpdateWarehouseProduct(int id, [FromBody] WarehouseProductDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return this.BadRequestError("Mismatched id.", "الرقم غير متطابق.");
            await _service.UpdateAsync(dto, ct);
            return this.OkSuccess("Warehouse Product Updated Successfully", "تم تحديث المنتج في المخزن بنجاح");
        }




        [HttpDelete("DeleteWarehouseProduct/{id:int}")]
        public async Task<IActionResult> DeleteWarehouseProduct(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return this.OkSuccess("Warehouse Product Deleted Successfully", "تم حذف المنتج في المخزن بنجاح");
        }








        //    [HttpGet("GetProductsByWarehouseCategoryAndCountry")]
        //    public async Task<IActionResult> GetProductsByWarehouseCategoryAndCountry(
        //[FromQuery] int warehouseId,
        //[FromQuery] int categoryId,
        //[FromQuery] int countryId,
        //CancellationToken ct)
        //    {
        //        var products = await _service.GetProductsByWarehouseCategoryAndCountryAsync(warehouseId, categoryId, countryId, ct);

        //        return this.OkSuccess(
        //            "Products fetched successfully.",
        //            "تم جلب المنتجات بنجاح.",
        //            products
        //        );
        //    }





        //[HttpGet("GetProductsByWarehouseCategoryAndCountry")]
        //public async Task<IActionResult> GetProductsByWarehouseCategoryAndCountry(
        //[FromQuery] int warehouseId,
        //[FromQuery] int categoryId,
        //[FromQuery] int countryId,
        //CancellationToken ct)
        //{
        //    var products = await _service.GetProductsWithPriceAsync(warehouseId, categoryId, countryId, ct);

        //    return this.OkSuccess(
        //        "Products fetched successfully.",
        //        "تم جلب المنتجات بنجاح.",
        //        products
        //    );
        //}




        [HttpGet("GetProductsByWarehouseCategoryAndCountry")]
        public async Task<IActionResult> GetProductsByWarehouseCategoryAndCountry(
        [FromQuery] int warehouseId,
        [FromQuery] int categoryId,
        CancellationToken ct)
        {
            var products = await _service.GetProductsByWarehouseAndCategoryAsync(warehouseId, categoryId, ct);

            return this.OkSuccess(
                "Products fetched successfully.",
                "تم جلب المنتجات بنجاح.",
                products
            );
        }




        [HttpDelete("DeleteByWarehouseId/{warehouseId:int}/AndProductId/{productId:int}")]
        public async Task<IActionResult> DeleteByWarehouseIdAndCategoryId(int warehouseId, int productId, CancellationToken ct)
        {
            await _service.DeleteByWarehouseIdAndProductId(warehouseId, productId, ct);
            return this.OkSuccess("Warehouse Category Deleted Successfully", "تم حذف فئة المخزن بنجاح");
        }


    }
}
