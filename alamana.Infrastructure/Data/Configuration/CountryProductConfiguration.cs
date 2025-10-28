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
    public class CountryProductConfiguration : IEntityTypeConfiguration<ProductCountryPrice>
    {

        public void Configure(EntityTypeBuilder<ProductCountryPrice> builder)
        {

            builder.HasIndex(x => new { x.CountryId, x.ProductId }).IsUnique();



        }
    }
}
