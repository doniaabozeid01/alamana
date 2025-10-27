using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Products.DTOs;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Core.Entities;

namespace alamana.Application.WarehouseCategory.Interfaces
{
    public interface IWarehouseCategoryService
    {
        Task<WarehouseCategoryDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CreateWarehouseCategoryDto dto, CancellationToken ct = default);
        Task UpdateAsync(WarehouseCategoryDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);


        Task<IReadOnlyList<categoriesInWarehouseDto?>> GetCategoriesByWarehouseId (int id, CancellationToken ct = default);
        Task DeleteByWarehouseIdAndCategoryId(int warehouseId, int categoryId, CancellationToken ct = default);

    }
}
