//using alamana.Application.Products.DTOs;
//using alamana.Application.Products.Interfaces;
using alamana.Application.Products.DTOs;
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
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehouseController(IWarehouseService service) => _service = service;


        [HttpGet("GetWarehouses")]
        public async Task<IActionResult> GetWarehouses([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        {
            var (items, total) = await _service.GetPagedAsync(search, page, pageSize, ct);

            var result = new
            {
                total,
                items,
                page,
                pageSize
            };

            return this.OkSuccess(
                "Warehouses fetched successfully",
                "تم جلب المخازن بنجاح",
                result
            );
        }



        [HttpGet("GetWarehouseById/{id:int}")]
        public async Task<IActionResult> GetWarehouseById(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null)
                return this.NotFoundError("Warehouse not found.", "المخزن غير موجودة");

            return this.OkSuccess("Warehouse fetched successfully", "تم جلب المخزن بنجاح", dto);
        }





        [HttpPost("CreateWarehouse")]
        public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseDto dto, CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            return this.OkSuccess("Warehouse Added Successfully", "تم اضافه المخزن بنجاح",id);
        }




        [HttpPut("UpdateWarehouse/{id:int}")]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] WarehouseDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return this.BadRequestError("Mismatched id.", "الرقم غير متطابق.");
            await _service.UpdateAsync(dto, ct);
            return this.OkSuccess("Warehouse Updated Successfully", "تم تحديث المخزن بنجاح");
        }


        [HttpDelete("DeleteWarehouse/{id:int}")]
        public async Task<IActionResult> DeleteWarehouse(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return this.OkSuccess("Warehouse Deleted Successfully", "تم حذف المخزن بنجاح");
        }



        [HttpPost("AssignWarehouseByProductsAndCategories")]
        public async Task<IActionResult> AssignWarehouseByProductsAndCategories ([FromBody] AssignWarehouseDto dto, CancellationToken ct)
        {
            await _service.AssignWarehouseByProductsAndCategories(dto, ct);
            return this.OkSuccess(
                "Warehouse categories and products assigned successfully.",
                "تم تعيين الفئات والمنتجات للمخزن بنجاح."
            );
        }


    }
}
