using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator:AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).
                NotEmpty()
                .GreaterThan(0)
                .WithName("Ürün Id");

            RuleFor(x => x.Title).
                NotEmpty()   //Not: Empty değerin "" olmasıdır. Null ise o değerin
                .WithName("Başlık");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithName("Açıklama");
            RuleFor(x => x.BrandId).NotEmpty()
                .GreaterThan(0).
                WithName("Marka");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithName("Fiyat");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .WithName("İndirim Oranı");

            RuleFor(x => x.CategoryIds)
                .NotEmpty().
                Must(categories => categories.Any()).
                WithName("Kategoriler");
        }
    }
}
