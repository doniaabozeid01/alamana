using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Products.DTOs;
using alamana.Application.Warehouses.DTOs;
using FluentValidation;

namespace alamana.Application.Warehouses.Validators
{
    public class UpdateWarehouseValidator : AbstractValidator<WarehouseDto>
    {
        public UpdateWarehouseValidator()
        { 
            RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Location).MaximumLength(500);
        }
    }
    
}
