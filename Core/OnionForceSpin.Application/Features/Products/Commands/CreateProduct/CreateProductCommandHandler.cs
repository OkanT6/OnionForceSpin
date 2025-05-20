using MediatR;
using Microsoft.AspNetCore.Http;
using OnionForceSpin.Application.Bases;
using OnionForceSpin.Application.Features.Products.Rules;
using OnionForceSpin.Application.Interfaces.AutoMapper;
using OnionForceSpin.Application.Interfaces.UnitOfWorks;
using OnionForceSpin.Domain.Entities;


//using OnionForceSpin.Application.Interfaces.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler :BaseHandler, IRequestHandler<CreateProductCommandRequest, Unit>
    {
        private readonly ProductRules productRules;


        public CreateProductCommandHandler(ProductRules productRules, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
            this.productRules = productRules;
        }
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {

            IList<Product> products = await unitOfWork.GetReadRepository<Product>().GetAllAsync();

            await productRules.ProductTitleMustNotBeSame(products, request.Title);

            Product product = new(request.Title,request.Description,request.BrandId,request.Price,request.Discount);

            await unitOfWork.GetWriteRepository<Product>().AddAsync(product);
            var result= await unitOfWork.SaveAsync();

            if (result > 0)
            {
                foreach(var categoryId in request.CategoryIds)
                {
                    await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
                    {
                        CategoryId = categoryId,
                        ProductId = product.Id
                    });
                }
                await unitOfWork.SaveAsync();
            }

            return Unit.Value;
        }
    }
}
