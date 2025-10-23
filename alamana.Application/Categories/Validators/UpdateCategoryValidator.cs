using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using alamana.Application.Categories.DTOs;

namespace alamana.Application.Categories.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.NameEn).NotEmpty().MaximumLength(150);
            RuleFor(x => x.NameAr).NotEmpty().MaximumLength(150);
            RuleFor(x => x.DescriptionEn).MaximumLength(500);
            RuleFor(x => x.DescriptionAr).MaximumLength(500);
        }
    }
}
