using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Categories.DTOs;
using alamana.Application.Products.DTOs;
using FluentValidation;

namespace alamana.Application.Products.Validators
{
    internal class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.NameEn).NotEmpty().MaximumLength(150);
            RuleFor(x => x.NameAr).NotEmpty().MaximumLength(150);
            RuleFor(x => x.DescriptionEn).MaximumLength(500);
            RuleFor(x => x.DescriptionAr).MaximumLength(500);
        }
    }
}
