using alamana.Application.Categories.DTOs;
using alamana.Application.Categories.Interfaces;
using alamana.Contracts.ApiResponses.Error;
using alamana.Contracts.ApiResponses.Success;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alamana.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) => _service = service;

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
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
                "Categories fetched successfully",
                "تم جلب الفئات بنجاح",
                result
            );
        }


        [HttpGet("GetCategoryById/{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null)
                return this.NotFoundError("Category not found.", "الفئة غير موجودة");

            return this.OkSuccess("Category fetched successfully", "تم جلب الفئة بنجاح", dto);
        }        

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto, CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            return this.OkSuccess("Category Added Successfully", "تم اضافه الفئه بنجاح");
        }

        [HttpPut("UpdateCategory/{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return this.BadRequestError("Mismatched id.", "الرقم غير متطابق.");
            await _service.UpdateAsync(dto, ct);
            return this.OkSuccess("Category Updated Successfully", "تم تحديث الفئه بنجاح");
        }

        [HttpDelete("DeleteCategory/{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return this.OkSuccess("Category Deleted Successfully", "تم حذف الفئه بنجاح");
        }



        [HttpGet("{categoryId:int}/products")]
        public async Task<IActionResult> GetProductsByCategoryIdId(int categoryId, CancellationToken ct)
        {
            var products = await _service.GetProductsByCountryId(categoryId, ct);
            if (products is null)
                return this.NotFoundError("products in this category aren't found.", "المنتجات في الفئه المختاره غير موجودة");

            return this.OkSuccess("Products in this category fetched successfully", "تم جلب  المنتجات في الفئه المختاره بنجاح", products);
        }



    }
}
