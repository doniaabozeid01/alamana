using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Products.DTOs;

namespace alamana.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto> Items, int Total)> GetPagedAsync(string? search, int page, int pageSize, CancellationToken ct = default);
        Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CreateProductDto dto, CancellationToken ct = default);
        Task UpdateAsync(UpdateProductDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
