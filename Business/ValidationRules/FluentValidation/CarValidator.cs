using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {

            RuleFor(p => p.DailyPrice).NotEmpty();
            RuleFor(p => p.ModelYear).NotEmpty();
            RuleFor(p => p.BrandId).NotEmpty();
            RuleFor(p => p.ColorId).NotEmpty();
            RuleFor(p => p.ModelYear).GreaterThan(2000);
            RuleFor(p => p.ModelYear).LessThan(2022);
            RuleFor(p => p.DailyPrice).GreaterThanOrEqualTo(500);

        }
    }
}
