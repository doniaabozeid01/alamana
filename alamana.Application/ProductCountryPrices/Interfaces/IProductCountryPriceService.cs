using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.ProductCountryPrices.DTOs;
using alamana.Application.Products.DTOs;

namespace alamana.Application.ProductCountryPrices.Interfaces
{
    public interface IProductCountryPriceService
    {
        Task<ProductCountryPriceDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CreateProductCountryPriceDto dto, CancellationToken ct = default);
        Task UpdateAsync(ProductCountryPriceDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<List<ProductDetailsWithPriceDto>> GetProductsByCountryIdAsync(int countryId, CancellationToken ct = default);

    }
}
