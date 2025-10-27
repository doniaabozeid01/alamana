using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Products.DTOs;
using alamana.Application.Warehouses.DTOs;

namespace alamana.Application.Warehouses.Interfaces
{
    public interface IWarehouseService
    {
        Task<(IEnumerable<WarehouseDto> Items, int Total)> GetPagedAsync(string? search, int page, int pageSize, CancellationToken ct = default);
        Task<WarehouseDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CreateWarehouseDto dto, CancellationToken ct = default);
        Task UpdateAsync(WarehouseDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);


        Task AssignWarehouseByProductsAndCategories (AssignWarehouseDto dto, CancellationToken ct = default);

    }
}
