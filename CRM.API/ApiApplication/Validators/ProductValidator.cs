using ApiApplication.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.Validators
{
    public class ProductValidator : AbstractValidator<ProductUpsertDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.MarkupRate).NotNull().NotEmpty().Must(x => x > 0).WithMessage("Marża musi być większa od zera.");
            RuleFor(x=>x.UnitValue).NotNull().NotEmpty().Must(x => x > 0).WithMessage("Wartość jednostkowa musi być większa od zera.");
            RuleFor(x=>x.VatRate).NotNull().NotEmpty().Must(x => x > 0).WithMessage("Wartość VAT musi być większa od zera.");
            RuleFor(x=>x.UnitOfMeasurement).NotNull().NotEmpty().WithMessage("Produkt musi posiadać określoną jednostkę miary.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Produkt musi mieć nazwę.");
        }
    }
}
