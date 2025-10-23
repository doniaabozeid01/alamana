//using alamana.Application.Categories.DTOs;
//using alamana.Application.Categories.Interfaces;
using alamana.Application.Countries.DTOs;
using alamana.Application.Products.DTOs;
using alamana.Application.Products.Interfaces;
using alamana.Contracts.ApiResponses.Error;
using alamana.Contracts.ApiResponses.Success;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alamana.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController (IProductService service) => _service = service;


        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
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
                "Products fetched successfully",
                "تم جلب المنتجات بنجاح",
                result
            );
        }



        [HttpGet("GetProductById/{id:int}")]
        public async Task<IActionResult> GetProductById(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null)
                return this.NotFoundError("Product not found.", "المنتج غير موجودة");

            return this.OkSuccess("Product fetched successfully", "تم جلب المنتج بنجاح", dto);
        }





        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto, CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            return this.OkSuccess("Product Added Successfully", "تم اضافه المنتج بنجاح");
        }




        [HttpPut("UpdateProduct/{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return this.BadRequestError("Mismatched id.", "الرقم غير متطابق.");
            await _service.UpdateAsync(dto, ct);
            return this.OkSuccess("Product Updated Successfully", "تم تحديث المنتج بنجاح");
        }




        [HttpDelete("DeleteProduct/{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return this.OkSuccess("Product Deleted Successfully", "تم حذف المنتج بنجاح");
        }
    }

}

