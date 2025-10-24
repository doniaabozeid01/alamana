using alamana.Application.Categories.DTOs;
using alamana.Application.Categories.Interfaces;
using alamana.Application.Countries.DTOs;
using alamana.Application.Countries.Interfaces;
using alamana.Contracts.ApiResponses.Error;
using alamana.Contracts.ApiResponses.Success;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alamana.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _service;

        public CountriesController(ICountryService service) => _service = service;

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries(
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
                "Countries fetched successfully",
                "تم جلب البلاد بنجاح",
                result
            );
        }




        [HttpGet("GetCountryById/{id:int}")]
        public async Task<IActionResult> GetCountryById(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto is null)
                return this.NotFoundError("Country not found.", "البلد غير موجودة");

            return this.OkSuccess("Country fetched successfully", "تم جلب البلد بنجاح", dto);
        }





        [HttpPost("CreateCountry")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCountryDto dto, CancellationToken ct)
        {
            var id = await _service.CreateAsync(dto, ct);
            return this.OkSuccess("Country Added Successfully", "تم اضافه البلد بنجاح");
        }





        [HttpPut("UpdateCountry/{id:int}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return this.BadRequestError("Mismatched id.", "الرقم غير متطابق.");
            await _service.UpdateAsync(dto, ct);
            return this.OkSuccess("Country Updated Successfully", "تم تحديث البلد بنجاح");
        }




        [HttpDelete("DeleteCountry/{id:int}")]
        public async Task<IActionResult> DeleteCountry(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return this.OkSuccess("Country Deleted Successfully", "تم حذف البلد بنجاح");
        }
    



        [HttpGet("{countryId:int}/warehouses")]
        public async Task<IActionResult> GetWarehousesByCountryId(int countryId, CancellationToken ct)
        {
            var warehouses = await _service.GetWarehousesByCountryId(countryId, ct);
            if (warehouses is null)
                return this.NotFoundError("Warehouses in this Country aren't found.", "المخازن في البلد المختاره غير موجودة");

            return this.OkSuccess("Warehouses in this Country fetched successfully", "تم جلب المخازن في البلد المختاره بنجاح", warehouses);
        }
    }
}



