using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace alamana.Infrastructure.Data.Configuration
{
    internal class WarehouseProductConfiguration : IEntityTypeConfiguration<WarehouseProducts>
    {

        public void Configure(EntityTypeBuilder<WarehouseProducts> builder)
        {

            builder.HasIndex(x => new { x.warehouseId, x.productId }).IsUnique();



        }
    }
}
