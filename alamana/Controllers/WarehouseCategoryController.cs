using alamana.Application.ProductCountryPrices.DTOs;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Application.WarehouseCategory.Interfaces;
using alamana.Application.Warehouses.DTOs;
using alamana.Application.Warehouses.Interfaces;
using alamana.Contracts.ApiResponses.Error;
using alamana.Contracts.ApiResponses.Success;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alamana.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseCategoryController : ControllerBase
    {
        private readonly IWarehouseCategoryService _service;

        public WarehouseCategoryController(IWarehouseCategoryService service) => _service = service;



        [HttpGet("GetWarehouseCategoryById/{id:int}")]
        public async Task<IActionResult> GetWarehouseCategoryById(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null)
                return this.NotFoundError("Warehouse Category not found.", "فئة المخزن غير موجود.");

            return this.OkSuccess("Warehouse Category fetched successfully", "تم جلب فئة المخزن بنجاح", dto);
        }





        [HttpPost("CreateWarehouseCategory")]
        public async Task<IActionResult> CreateWarehouseCategory([FromBody] CreateWarehouseCategoryDto dto, CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            return this.OkSuccess("Warehouse Category Added Successfully", "تم اضافه فئة المخزن بنجاح");
        }





        [HttpPut("UpdateWarehouseCategory/{id:int}")]
        public async Task<IActionResult> UpdateWarehouseCategory(int id, [FromBody] WarehouseCategoryDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return this.BadRequestError("Mismatched id.", "الرقم غير متطابق.");
            await _service.UpdateAsync(dto, ct);
            return this.OkSuccess("Warehouse Category Updated Successfully", "تم تحديث فئة المخزن بنجاح");
        }




        [HttpDelete("DeleteWarehouseCategory/{id:int}")]
        public async Task<IActionResult> DeleteWarehouseCategory(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return this.OkSuccess("Warehouse Category Deleted Successfully", "تم حذف فئة المخزن بنجاح");
        }




        [HttpGet("GetCategoriesByWarehouseId/{id:int}")]
        public async Task<IActionResult> GetCategoriesByWarehouseId (int id, CancellationToken ct)
        {
            var dto = await _service.GetCategoriesByWarehouseId(id, ct);
            if (dto is null)
                return this.NotFoundError("Warehouse Category not found.", "فئة المخزن غير موجود.");

            return this.OkSuccess("Warehouse Categories fetched successfully", "تم جلب فئات المخزن بنجاح", dto);
        }





        [HttpDelete("DeleteByWarehouseId/{warehouseId:int}/AndCategoryId/{categoryId:int}")]
        public async Task<IActionResult> DeleteByWarehouseIdAndCategoryId(int warehouseId, int categoryId, CancellationToken ct)
        {
            await _service.DeleteByWarehouseIdAndCategoryId(warehouseId,categoryId, ct);
            return this.OkSuccess("Warehouse Category Deleted Successfully", "تم حذف فئة المخزن بنجاح");
        }









    }
}
