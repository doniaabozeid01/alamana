using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Products.DTOs;
using alamana.Application.WarehouseCategory.Interfaces;
using alamana.Core.Entities;
using alamana.Core.Interfaces.Products;
using alamana.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using alamana.Application.WarehouseCategory.DTOs;
using AutoMapper.QueryableExtensions;
using alamana.Core.Interfaces.WarehouseCategory;
using alamana.Application.Common.Exceptions;

namespace alamana.Application.WarehouseCategory.Services
{
    public class WarehouseCategoryService : IWarehouseCategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IWarehouseCategoryRepository _repo; // نستخدم المخصص

        public WarehouseCategoryService(IUnitOfWork uow, IMapper mapper, IWarehouseCategoryRepository repo)
        {
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<WarehouseCategoryDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var WarehouseCategory = await _repo.GetByIdAsync(id, ct);
            if (WarehouseCategory is null)
                throw new NotFoundException(
                    $"Warehouse Category with ID {id} not found.",
                    $"فئة المخزن ذات المعرف {id} غير موجودة."
                );

            return _mapper.Map<WarehouseCategoryDto>(WarehouseCategory);
        }

        public async Task<int> CreateAsync(CreateWarehouseCategoryDto dto, CancellationToken ct = default)
        {
            // فحص تكرار الاسم
            var entity = _mapper.Map<WarehouseCategories>(dto);

            // توليد Slug بسيط (اختياري)

            await _repo.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(WarehouseCategoryDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(dto.Id, ct)
                ?? throw new NotFoundException(
    $"Warehouse Category with ID {dto.Id} not found.",
    $"فئة المخزن ذات المعرف {dto.Id} غير موجودة."
);


            // منع جعل الـParent هو نفسه أو أحد الأبناء المباشرين (فحص بسيط)



            // ماب
            entity.categoryId = dto.categoryId;
            entity.warehouseId = dto.warehouseId;

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
    $"Warehouse Category with ID {id} not found.",
    $"فئة المخزن ذات المعرف {id} غير موجودة."
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
