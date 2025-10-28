using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using alamana.Core.Entities;
using alamana.Infrastructure.Data.Configuration;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using alamana.Infrastructure.Identity;

namespace alamana.Infrastructure.Data
{
    public class AlamanaDbContext : IdentityDbContext<AppUser, AppRole, Guid> // ✅ لاحظ Guid هنا

    {

        public AlamanaDbContext (DbContextOptions<AlamanaDbContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCountryPrice> ProductCountryPrices => Set<ProductCountryPrice>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<WarehouseCategories> WarehouseCategories => Set<WarehouseCategories>();
        public DbSet<WarehouseProducts> WarehouseProducts => Set<WarehouseProducts>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseProductConfiguration());
            modelBuilder.ApplyConfiguration(new CountryProductConfiguration());

        }


    }
}
