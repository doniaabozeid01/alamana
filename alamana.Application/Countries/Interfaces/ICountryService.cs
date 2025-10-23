using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Categories.DTOs;
using alamana.Application.Countries.DTOs;

namespace alamana.Application.Countries.Interfaces
{
    public interface ICountryService
    {
        Task<(IEnumerable<CountryDto> Items, int Total)> GetPagedAsync(string? search, int page, int pageSize, CancellationToken ct = default);
        Task<CountryDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CreateCountryDto dto, CancellationToken ct = default);
        Task UpdateAsync(CountryDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
