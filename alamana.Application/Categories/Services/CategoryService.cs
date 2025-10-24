using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Categories.DTOs;
using alamana.Application.Categories.Interfaces;
using alamana.Application.Common.Exceptions;
using alamana.Application.Countries.DTOs;
using alamana.Application.Products.DTOs;
using alamana.Core.Entities;
using alamana.Core.Interfaces;
using alamana.Core.Interfaces.Categories;
using alamana.Core.Interfaces.Products;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace alamana.Application.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repo; // نستخدم المخصص
        private readonly IProductRepository _productRepository;

        public CategoryService(IUnitOfWork uow, IMapper mapper, ICategoryRepository repo, IProductRepository productRepository)
        {
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
            _productRepository = productRepository;
        }

        public async Task<(IEnumerable<CategoryDto> Items, int Total)> GetPagedAsync(string? search, int page, int pageSize, CancellationToken ct = default)
        {
            var q = _repo.Query();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(c => c.NameEn.Contains(search) || c.NameAr.Contains(search));

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {

            var category = await _repo.GetByIdAsync(id, ct);
            if (category is null)
                throw new NotFoundException($"Category with ID {id} not found.", $"الفئة ذات المعرف {id} غير موجودة.");

            return _mapper.Map<CategoryDto>(category);

        }

        public async Task<int> CreateAsync(CreateCategoryDto dto, CancellationToken ct = default)
        {
            // فحص تكرار الاسم
            if (await _repo.ExistsByNameAsync(dto.NameEn,dto.NameAr, null, ct))
                throw new ConflictException(
                    "Category name already exists.",
                    "اسم الفئة موجود بالفعل."
                );



            var entity = _mapper.Map<Category>(dto);

            // توليد Slug بسيط (اختياري)
            entity.Slug = GenerateSlug(dto.NameEn);

            await _repo.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return entity.Id;
        }

        
        public async Task UpdateAsync(UpdateCategoryDto dto, CancellationToken ct = default)
        {
            // 1) موجود ولا لأ؟
            var entity = await _repo.GetByIdAsync(dto.Id, ct)
            ?? throw new NotFoundException(
                $"Category with ID {dto.Id} not found.",
                $"الفئة ذات المعرف {dto.Id} غير موجودة."
            );



            if (await _repo.ExistsByNameAsync(dto.NameEn, dto.NameAr, dto.Id, ct))
                throw new ConflictException(
                                    "Category name already exists.",
                                    "اسم الفئة موجود بالفعل."
                                );


            // 4) تحديث الحقول
            entity.NameEn = dto.NameEn.Trim();
            entity.NameAr = dto.NameAr?.Trim();
            entity.DescriptionEn = dto.DescriptionEn;
            entity.DescriptionAr = dto.DescriptionAr;
            entity.IsActive = dto.IsActive;

            // 5) Slug (وتأكد عدم تكراره لغير نفس الـId)
            var newSlug = GenerateSlug(dto.NameEn);
            var slugTaken = await _repo.Query().AnyAsync(c => c.Id != dto.Id && c.Slug == newSlug, ct);
            if (slugTaken)
                throw new ConflictException(
                    "Slug already exists for another category.",
                    "الرمز التعريفي (Slug) مستخدم بالفعل في فئة أخرى."
                );
            entity.Slug = newSlug;

            entity.UpdatedAt = DateTime.UtcNow; // لو عندك تتبع وقتي في BaseEntity

            _repo.Update(entity);
            await _uow.SaveChangesAsync(ct);
        }






        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.Query()
                //.Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.Id == id, ct)
?? throw new NotFoundException(
    "Category not found.",
    "الفئة غير موجودة."
);

            //if (entity.Children.Any())
            //    throw new InvalidOperationException("Cannot delete a category that has child categories.");

            //// Soft delete
            //entity.IsDeleted = true;
            _repo.Delete(entity);

            await _uow.SaveChangesAsync(ct);
        }

        // util
        private static string GenerateSlug(string name)
        {
            var s = name.Trim().ToLowerInvariant();
            s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", "-");
            s = System.Text.RegularExpressions.Regex.Replace(s, @"[^a-z0-9\-]", "");
            return s;
        }





        public async Task<CategoryWithProductsDto?> GetProductsByCountryId (int categoryId, CancellationToken ct = default)
        {
            var category = await this.GetByIdAsync(categoryId);
            var products = await _productRepository.GetProductsByCategoryId(categoryId);
            if (products is null || !products.Any())
                throw new NotFoundException(
                    $"Products in this Category aren't found.",
                    "المنتجات في الفئه المختارة غير موجودة."
                );

            var productsDtos = products.Select(w => new ProductDto
            {
                Id = w.Id,
                NameEn = w.NameEn,
                NameAr = w.NameAr,
                DescriptionEn = w.DescriptionEn,
                DescriptionAr = w.DescriptionAr,
                Slug = w.Slug,
            }).ToList();


            var productsCategory = new CategoryWithProductsDto
            {
                Id = category.Id,
                NameEn = category.NameEn,
                NameAr = category.NameAr,
                DescriptionEn = category.DescriptionEn, 
                DescriptionAr = category.DescriptionAr,
                products = productsDtos,
            };

            return productsCategory;
        }

    }
}
