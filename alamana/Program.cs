using alamana.Application.Categories.Interfaces;
using alamana.Application.Categories.Mapping;
using alamana.Application.Categories.Services;
using alamana.Application.Categories.Validators;
using alamana.Application.Countries.Interfaces;
using alamana.Application.Countries.Mapping;
using alamana.Application.Countries.Services;
using alamana.Application.ProductCountryPrices.Interfaces;
using alamana.Application.ProductCountryPrices.Mapping;
using alamana.Application.ProductCountryPrices.Services;
using alamana.Application.Products.Interfaces;
using alamana.Application.Products.Mapping;
using alamana.Application.Products.Services;
using alamana.Application.Products.Validators;
using alamana.Application.WarehouseCategory.Interfaces;
using alamana.Application.WarehouseCategory.Mapping;
using alamana.Application.WarehouseCategory.Services;
using alamana.Application.WarehouseProduct.Interfaces;
using alamana.Application.WarehouseProduct.Mapping;
using alamana.Application.WarehouseProduct.Services;
using alamana.Application.Warehouses.Interfaces;
using alamana.Application.Warehouses.Mapping;
using alamana.Application.Warehouses.Services;
using alamana.Core.Interfaces;
using alamana.Core.Interfaces.Categories;
using alamana.Core.Interfaces.Countries;
using alamana.Core.Interfaces.ProductCountryPrices;
using alamana.Core.Interfaces.Products;
using alamana.Core.Interfaces.WarehouseCategory;
using alamana.Core.Interfaces.WarehouseProduct;
using alamana.Core.Interfaces.Warehouses;
using alamana.Infrastructure.Data;
using alamana.Infrastructure.Repositories;
using alamana.Infrastructure.Repositories.Categories;
using alamana.Infrastructure.Repositories.Countries;
using alamana.Infrastructure.Repositories.ProductCountryPrices;
using alamana.Infrastructure.Repositories.Products;
using alamana.Infrastructure.Repositories.WarehouseCategory;
using alamana.Infrastructure.Repositories.WarehouseProduct;
using alamana.Infrastructure.Repositories.Warehouses;
using alamana.Middlewares;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AlamanaDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// UoW + Generic
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IWarehouseCategoryRepository, WarehouseCategoryRepository>();
builder.Services.AddScoped<IWarehouseProductRepository, WarehouseProductRepository>();
builder.Services.AddScoped<IProductCountryPriceRepository, ProductCountryPriceRepository>();




// Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IWarehouseCategoryService, WarehouseCategoryService>();
builder.Services.AddScoped<IWarehouseProductService, WarehouseProductService>();
builder.Services.AddScoped<IProductCountryPriceService, ProductCountryPriceService>();





//builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(WarehouseProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(CountryProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(WarehouseCategoryProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(WarehouseProductProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(ProductCountryPriceProfile).Assembly);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



builder.Services.AddValidatorsFromAssembly(typeof(CreateCategoryValidator).Assembly);






builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
