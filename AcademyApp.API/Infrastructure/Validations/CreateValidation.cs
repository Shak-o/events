using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.API.Infrastructure.Models;
using FluentValidation;

namespace AcademyApp.API.Infrastructure.Validations
{
    public class CreateValidation : AbstractValidator<EventPostModel>
    {
        public CreateValidation()
        {
            RuleFor(e => e.Title).NotEmpty().MaximumLength(50);
            RuleFor(e => e.Description).NotEmpty().MaximumLength(500);
            RuleFor(e => e.EndDate).NotEmpty();
            RuleFor(e => e.StartDate).NotEmpty();
        }
    }
}
