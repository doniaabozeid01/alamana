using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Categories.DTOs;
using alamana.Application.Common.Exceptions;
using alamana.Application.ProductCountryPrices.DTOs;
using alamana.Application.ProductCountryPrices.Interfaces;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Core.Entities;
using alamana.Core.Interfaces;
using alamana.Core.Interfaces.ProductCountryPrices;
using alamana.Core.Interfaces.WarehouseCategory;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace alamana.Application.ProductCountryPrices.Services
{
    public class ProductCountryPriceService : IProductCountryPriceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IProductCountryPriceRepository _repo; // نستخدم المخصص

        public ProductCountryPriceService(IUnitOfWork uow, IMapper mapper, IProductCountryPriceRepository repo)
        {
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ProductCountryPriceDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var ProductCountryPrice = await _repo.GetByIdAsync(id, ct);
            if (ProductCountryPrice is null)
                throw new NotFoundException(
                    $"Product Country Price with ID {id} not found.",
                    $"سعر المنتج في البلد المحددة بالمعرف {id} غير موجود."
                );

            return _mapper.Map<ProductCountryPriceDto>(ProductCountryPrice);
        }

        public async Task<int> CreateAsync(CreateProductCountryPriceDto dto, CancellationToken ct = default)
        {
            // فحص تكرار الاسم
            var entity = _mapper.Map<ProductCountryPrice>(dto);

            // توليد Slug بسيط (اختياري)

            await _repo.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(ProductCountryPriceDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(dto.Id, ct)
                ?? throw new NotFoundException(
    $"Product Country Price with ID {dto.Id} not found.",
    $"سعر المنتج في البلد المحددة بالمعرف {dto.Id} غير موجود."
);


            // ماب
            entity.ProductId = dto.ProductId;
            entity.CountryId = dto.CountryId;
            entity.Amount = dto.Amount;

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
    $"Product Country Price with ID {id} not found.",
    $"سعر المنتج في البلد المحددة بالمعرف {id} غير موجود."
);
            //if (entity.Children.Any())
            //    throw new InvalidOperationException("Cannot delete a category that has child categories.");

            //// Soft delete
            //entity.IsDeleted = true;
            _repo.Delete(entity);

            await _uow.SaveChangesAsync(ct);
        }

    }
}
