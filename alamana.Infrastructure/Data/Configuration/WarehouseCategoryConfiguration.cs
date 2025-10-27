using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace alamana.Infrastructure.Data.Configuration
{
    public class WarehouseCategoryConfiguration : IEntityTypeConfiguration<WarehouseCategories>
    {

        public void Configure(EntityTypeBuilder<WarehouseCategories> builder)
        {

            builder.HasIndex(x => new { x.warehouseId, x.categoryId }).IsUnique();



        }
    }
}
