using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Application.Categories.DTOs;
using alamana.Application.Countries.DTOs;
using FluentValidation;

namespace alamana.Application.Countries.Validators
{
    public class CreateCountryValidator : AbstractValidator<CreateCountryDto>
    {
        public CreateCountryValidator()
        {
            RuleFor(x => x.NameEn).NotEmpty().MaximumLength(150);
            RuleFor(x => x.NameAr).NotEmpty().MaximumLength(150);
        }
    }
}
