using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using alamana.Application.Categories.DTOs;
using alamana.Application.Products.Interfaces;
using alamana.Core.Entities;
//using alamana.Core.Interfaces.Categories;
using alamana.Core.Interfaces;
using AutoMapper;
using alamana.Application.Products.DTOs;
using alamana.Core.Interfaces.Products;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using alamana.Application.Categories.DTOs;
using alamana.Application.Common.Exceptions;

namespace alamana.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repo; // نستخدم المخصص

        public ProductService(IUnitOfWork uow, IMapper mapper, IProductRepository repo)
        {
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<(IEnumerable<ProductDto> Items, int Total)> GetPagedAsync(string? search, int page, int pageSize, CancellationToken ct = default)
        {
            var q = _repo.Query();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(c => c.NameEn.Contains(search) || c.NameAr.Contains(search));

            var total = await q.CountAsync(ct);

            var items = await q
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Product = await _repo.GetByIdAsync(id, ct);
            if (Product is null)
                throw new NotFoundException($"Product with ID {id} not found.", $"المنتج ذات المعرف {id} غير موجودة.");

            return _mapper.Map<ProductDto>(Product);
        }



        public async Task<IReadOnlyList<ProductDto?>> GetAllAsync(CancellationToken ct = default)
        {
            var Product = await _repo.GetAllAsync(ct);
            if (Product is null)
                throw new NotFoundException($"Products not found.", $"المنتجات غير موجودة.");

            return _mapper.Map<IReadOnlyList<ProductDto>>(Product);
        }



        public async Task<int> CreateAsync(CreateProductDto dto, CancellationToken ct = default)
        {
            // فحص تكرار الاسم
            if (await _repo.ExistsByNameAsync(dto.NameEn, dto.NameAr, null, ct))
                throw new ConflictException(
                    "Product name already exists.",
                    "اسم المنتج موجود بالفعل."
                );



            var entity = _mapper.Map<Product>(dto);

            // توليد Slug بسيط (اختياري)
            entity.Slug = GenerateSlug(dto.NameEn);

            await _repo.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(UpdateProductDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(dto.Id, ct)
                ?? throw new NotFoundException($"Product with ID {dto.Id} not found.", $"المنتج ذات المعرف {dto.Id} غير موجودة.");


            if (await _repo.ExistsByNameAsync(dto.NameEn, dto.NameAr, dto.Id, ct))
                throw new ConflictException(
                    "Product name already exists.",
                    "اسم المنتج موجود بالفعل."
                );
            // منع جعل الـParent هو نفسه أو أحد الأبناء المباشرين (فحص بسيط)



            // ماب
            entity.NameEn = dto.NameEn;
            entity.NameAr = dto.NameAr;
            entity.DescriptionEn = dto.DescriptionEn;
            entity.DescriptionAr = dto.DescriptionAr;
            entity.CategoryId = dto.CategoryId;

            // تحديث الـSlug لو الاسم اتغير
            entity.Slug = GenerateSlug(dto.NameEn);

            _repo.Update(entity);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.Query()
                //.Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.Id == id, ct)
                ?? throw new NotFoundException($"Product with ID {id} not found.", $"المنتج ذات المعرف {id} غير موجودة.");


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
    }
}
