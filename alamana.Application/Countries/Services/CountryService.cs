using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Categories.DTOs;
using alamana.Application.Common.Exceptions;
using alamana.Application.Countries.DTOs;

//using alamana.Application.Categories.DTOs;
using alamana.Application.Countries.Interfaces;
using alamana.Application.Warehouses.DTOs;
using alamana.Core.Entities;
//using alamana.Core.Interfaces.Categories;
using alamana.Core.Interfaces;
using alamana.Core.Interfaces.Countries;
using alamana.Core.Interfaces.Warehouses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace alamana.Application.Countries.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICountryRepository _repo; // نستخدم المخصص


        private readonly IWarehouseRepository _warehouseRepository;

        public CountryService(IUnitOfWork uow, IMapper mapper, ICountryRepository repo, IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<(IEnumerable<CountryDto> Items, int Total)> GetPagedAsync(string? search, int page, int pageSize, CancellationToken ct = default)
        {
            var q = _repo.Query();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(c => c.NameEn.Contains(search) || c.NameAr.Contains(search));

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<CountryDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Country = await _repo.GetByIdAsync(id, ct);
            if (Country is null)
                throw new NotFoundException(
                    $"Country with ID {id} not found.",
                    $"الدولة ذات المعرف {id} غير موجودة."
                );

            return _mapper.Map<CountryDto>(Country);
        }






        public async Task<int> CreateAsync(CreateCountryDto dto, CancellationToken ct = default)
        {
            // فحص تكرار الاسم
            if (await _repo.ExistsByNameAsync(dto.NameEn, dto.NameAr, null, ct))
                throw new ConflictException(
                    "Country name already exists.",
                    "اسم الدولة موجود بالفعل."
                );



            var entity = _mapper.Map<Country>(dto);

            // توليد Slug بسيط (اختياري)

            await _repo.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(CountryDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(dto.Id, ct)
?? throw new NotFoundException(
    "Country not found.",
    "الدولة غير موجودة."
);

            if (await _repo.ExistsByNameAsync(dto.NameEn, dto.NameAr, dto.Id, ct))
                throw new ConflictException(
                                    "Country name already exists.",
                                    "اسم الدولة موجود بالفعل."
                                );
            // منع جعل الـParent هو نفسه أو أحد الأبناء المباشرين (فحص بسيط)



            // ماب
            entity.NameEn = dto.NameEn;
            entity.NameAr = dto.NameAr;
            entity.CountryCode = dto.CountryCode;
            entity.CurrencyEn = dto.CurrencyEn;
            entity.CurrencyAr = dto.CurrencyAr;

            // تحديث الـSlug لو الاسم اتغير

            _repo.Update(entity);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.Query()
                //.Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.Id == id, ct)
?? throw new NotFoundException(
    "Country not found.",
    "الدولة غير موجودة."
);

            //if (entity.Children.Any())
            //    throw new InvalidOperationException("Cannot delete a category that has child categories.");

            //// Soft delete
            //entity.IsDeleted = true;
            _repo.Delete(entity);

            await _uow.SaveChangesAsync(ct);
        }

        public async Task<CountryWithWarehouseDto?> GetWarehousesByCountryId(int countryId, CancellationToken ct = default)
        {
            var Country = await this.GetByIdAsync(countryId);
            var Warehouses =await _warehouseRepository.GetWarehousesByCountryId(countryId);
            if (Warehouses is null || !Warehouses.Any())
                throw new NotFoundException(
                    $"Warehouses in this Country aren't found.",
                    "المخازن في البلد المختارة غير موجودة."
                );

            var warehouseDtos = Warehouses.Select(w => new WarehouseDto
            {
                Id = w.Id,
                Name = w.Name,
                Location = w.Location
            }).ToList();


            var warehousesCountry = new CountryWithWarehouseDto
            {
                Id = countryId,
                NameEn = Country.NameEn,
                NameAr = Country.NameAr,
                Warehouses = warehouseDtos
            };

            return warehousesCountry;
        }
    }
}
