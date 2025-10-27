using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Categories.DTOs;

namespace alamana.Application.Categories.Interfaces
{
    public interface ICategoryService
    {
        Task<(IEnumerable<CategoryDto> Items, int Total)> GetPagedAsync(string? search, int page, int pageSize, CancellationToken ct = default);
        Task<IEnumerable<CategoryProductsDto>> getCategoriesWithProducts(CancellationToken ct = default);
        Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CreateCategoryDto dto, CancellationToken ct = default);
        Task UpdateAsync(UpdateCategoryDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<CategoryWithProductsDto?> GetProductsByCategoryId (int categoryId, CancellationToken ct = default);

    }
}
