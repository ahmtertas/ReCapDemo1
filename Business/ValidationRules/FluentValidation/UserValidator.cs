using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator /*: AbstractValidator*/
    {
        //public UserValidator()
        //{
        //    RuleFor(p => p.FirstName).NotEmpty().WithMessage("İsim boş olamaz.");
        //    RuleFor(p => p.LastName).NotEmpty();
        //    RuleFor(p => p.Email).NotEmpty();
        //    RuleFor(p => p.Password).NotEmpty();
        //    RuleFor(p => p.FirstName).MinimumLength(2);
        //    RuleFor(p => p.LastName).MinimumLength(2);
        //    RuleFor(p => p.Password).MinimumLength(6);
        //    RuleFor(p => p.Email).Must(EndWithGmailCom);
        //}

        //private bool EndWithGmailCom(string arg)
        //{
        //    return arg.EndsWith("@gmail.com");
        //}
    }
}
