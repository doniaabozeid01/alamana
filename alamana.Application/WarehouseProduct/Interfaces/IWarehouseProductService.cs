using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Application.WarehouseProduct.DTOs;

namespace alamana.Application.WarehouseProduct.Interfaces
{
    public interface IWarehouseProductService
    {
        Task<WarehouseProductDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CreateWarehouseProductDto dto, CancellationToken ct = default);
        Task UpdateAsync(WarehouseProductDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);

        //    Task<List<ProductWithPriceDto>> GetProductsByWarehouseCategoryAndCountryAsync(
        //int warehouseId, int categoryId, int countryId, CancellationToken ct = default);

        Task<List<ProductWithPriceDto>> GetProductsWithPriceAsync(
            int warehouseId, int categoryId, int countryId, CancellationToken ct);
    }
}
