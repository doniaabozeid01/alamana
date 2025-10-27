using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Products.DTOs;
using alamana.Core.Entities;
using alamana.Core.Interfaces.Products;
using alamana.Core.Interfaces;
using AutoMapper;
using alamana.Core.Interfaces.Warehouses;
using alamana.Application.Warehouses.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using alamana.Application.Warehouses.Interfaces;
using alamana.Application.Common.Exceptions;
using alamana.Core.Interfaces.WarehouseCategory;
using alamana.Core.Interfaces.WarehouseProduct;

namespace alamana.Application.Warehouses.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IWarehouseRepository _repo; // نستخدم المخصص
        private readonly IWarehouseCategoryRepository _warehouseCategoryRepo;
        private readonly IWarehouseProductRepository _warehouseProductRepo;


        //private readonly IGenericRepository<WarehouseCategory> _warehouseCategoryRepo;
        //private readonly IGenericRepository<WarehouseProduct> _warehouseProductRepo;
        //private readonly IGenericRepository<Warehouse> _warehouseRepo;

        public WarehouseService(IUnitOfWork uow, IMapper mapper, IWarehouseRepository repo, IWarehouseCategoryRepository warehouseCategoryRepository, IWarehouseProductRepository warehouseProductRepository)
        {
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
            _warehouseProductRepo = warehouseProductRepository;
            _warehouseCategoryRepo = warehouseCategoryRepository;
        }

        public async Task<(IEnumerable<WarehouseDto> Items, int Total)> GetPagedAsync(string? search, int page, int pageSize, CancellationToken ct = default)
        {
            var q = _repo.Query();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(c => c.Name.Contains(search));

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<WarehouseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<WarehouseDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {

            var Warehouse = await _repo.GetByIdAsync(id, ct);
            if (Warehouse is null)
                throw new NotFoundException(
                    $"Warehouse with ID {id} not found.",
                    $"المخزن ذو المعرف {id} غير موجود."
                );

            return _mapper.Map<WarehouseDto>(Warehouse);
        }

        public async Task<int> CreateAsync(CreateWarehouseDto dto, CancellationToken ct = default)
        {
            // فحص تكرار الاسم
            if (await _repo.ExistsByNameAsync(dto.Name, null, ct))
                throw new ConflictException(
                    "Warehouse name already exists.",
                    "اسم المخزن موجود بالفعل."
                );



            var entity = _mapper.Map<Warehouse>(dto);

            // توليد Slug بسيط (اختياري)
            //entity.Slug = GenerateSlug(dto.Name);

            await _repo.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(WarehouseDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(dto.Id, ct)
                ?? throw new NotFoundException(
                    $"Warehouse with ID {dto.Id} not found.",
                    $"المخزن ذو المعرف {dto.Id} غير موجود."
                );

            if (await _repo.ExistsByNameAsync(dto.Name, dto.Id, ct))
                throw new ConflictException(
                    "Warehouse name already exists.",
                    "اسم المخزن موجود بالفعل."
                );

            // منع جعل الـParent هو نفسه أو أحد الأبناء المباشرين (فحص بسيط)



            // ماب
            entity.Name = dto.Name;
            //entity.NameAr = dto.NameAr;
            //entity.DescriptionEn = dto.DescriptionEn;
            //entity.DescriptionAr = dto.DescriptionAr;
            entity.Location = dto.Location;
            entity.CountryId = dto.CountryId;

            // تحديث الـSlug لو الاسم اتغير
            //entity.Slug = GenerateSlug(dto.NameEn);

            _repo.Update(entity);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.Query()
                //.Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.Id == id, ct)
?? throw new NotFoundException(
    $"Warehouse with ID {id} not found.",
    $"المخزن ذو المعرف {id} غير موجود."
);
            //if (entity.Children.Any())
            //    throw new InvalidOperationException("Cannot delete a category that has child categories.");

            //// Soft delete
            //entity.IsDeleted = true;
            _repo.Delete(entity);

            await _uow.SaveChangesAsync(ct);
        }

        //public Task AssignWarehouseByProductsAndCategories(AssignWarehouseDto dto, CancellationToken ct = default)
        //{

        //}






        public async Task AssignWarehouseByProductsAndCategories(AssignWarehouseDto dto, CancellationToken ct = default)
        {
            // ✅ 1. تحقّق من وجود المخزن
            var warehouse = await _repo.GetByIdAsync(dto.warehouseId, ct);
            if (warehouse == null)
                throw new KeyNotFoundException($"Warehouse {dto.warehouseId} not found.");

            // ✅ 2. إعداد القوائم
            var targetCategoryIds = dto.CategoriesIds?.Distinct().ToList() ?? new List<int>();
            var targetProductIds = dto.ProductsIds?.Distinct().ToList() ?? new List<int>();

            // ✅ 3. WarehouseCategory
            var existingCatLinks = await _warehouseCategoryRepo.Query()
                .Where(x => x.warehouseId == dto.warehouseId)
                .ToListAsync(ct);

            var existingCatIds = existingCatLinks.Select(x => x.categoryId).ToHashSet();

            var catIdsToAdd = targetCategoryIds.Except(existingCatIds).ToList();
            var catLinksToRemove = existingCatLinks.Where(x => !targetCategoryIds.Contains(x.categoryId)).ToList();

            if (catLinksToRemove.Count > 0)
            {
                _warehouseCategoryRepo.DeleteRange(catLinksToRemove);
                await _uow.SaveChangesAsync(ct);
            }

            if (catIdsToAdd.Count > 0)
            {
                var newCatLinks = catIdsToAdd.Select(id => new WarehouseCategories
                {
                    warehouseId = dto.warehouseId,
                    categoryId = id
                }).ToList();

                await _warehouseCategoryRepo.AddRangeAsync(newCatLinks, ct);
            }

            // ✅ 4. WarehouseProduct
            var existingProdLinks = await _warehouseProductRepo
                .FindAsync(x => x.warehouseId == dto.warehouseId, ct);

            var existingProdIds = existingProdLinks.Select(x => x.productId).ToHashSet();

            var prodIdsToAdd = targetProductIds.Except(existingProdIds).ToList();
            var prodLinksToRemove = existingProdLinks.Where(x => !targetProductIds.Contains(x.productId)).ToList();

            if (prodLinksToRemove.Count > 0)
                 _warehouseProductRepo.DeleteRange(prodLinksToRemove);

            if (prodIdsToAdd.Count > 0)
            {
                var newProdLinks = prodIdsToAdd.Select(id => new WarehouseProducts
                {
                    warehouseId = dto.warehouseId,
                    productId = id
                }).ToList();

                await _warehouseProductRepo.AddRangeAsync(newProdLinks, ct);
            }

            // ✅ 5. الحفظ
            await _uow.SaveChangesAsync(ct);

        }


    }
}
