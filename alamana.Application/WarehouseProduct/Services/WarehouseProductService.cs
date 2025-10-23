using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Common.Exceptions;
using alamana.Application.Products.DTOs;
using alamana.Application.WarehouseCategory.DTOs;
using alamana.Application.WarehouseProduct.DTOs;

//using alamana.Application.WarehouseCategory.DTOs;
using alamana.Application.WarehouseProduct.Interfaces;
using alamana.Core.Entities;
//using alamana.Core.Interfaces.WarehouseCategory;
using alamana.Core.Interfaces;
using alamana.Core.Interfaces.WarehouseProduct;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace alamana.Application.WarehouseProduct.Services
{
    public class WarehouseProductService : IWarehouseProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IWarehouseProductRepository _repo; // نستخدم المخصص

        public WarehouseProductService(IUnitOfWork uow, IMapper mapper, IWarehouseProductRepository repo)
        {
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<WarehouseProductDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var WarehouseProduct = await _repo.GetByIdAsync(id, ct);
            if (WarehouseProduct is null)
                throw new NotFoundException(
                    $"Warehouse Product with ID {id} not found.",
                    $"المنتج في المخزن ذو المعرف {id} غير موجود."
                );

            return _mapper.Map<WarehouseProductDto>(WarehouseProduct);
        }

        public async Task<int> CreateAsync(CreateWarehouseProductDto dto, CancellationToken ct = default)
        {
            // فحص تكرار الاسم
            var entity = _mapper.Map<WarehouseProducts>(dto);

            // توليد Slug بسيط (اختياري)

            await _repo.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(WarehouseProductDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(dto.Id, ct)
?? throw new NotFoundException(
    $"Warehouse Product with ID {dto.Id} not found.",
    $"المنتج في المخزن ذو المعرف {dto.Id} غير موجود."
);


            // منع جعل الـParent هو نفسه أو أحد الأبناء المباشرين (فحص بسيط)



            // ماب
            entity.productId = dto.productId;
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
    $"Warehouse Product with ID {id} not found.",
    $"المنتج في المخزن ذو المعرف {id} غير موجود."
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
