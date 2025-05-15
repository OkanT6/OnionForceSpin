using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionForceSpin.Application.DTOs;
using OnionForceSpin.Application.Interfaces.AutoMapper;
using OnionForceSpin.Application.Interfaces.UnitOfWorks;
using OnionForceSpin.Domain.Entities;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork; //UnitOfWork'lerimizi application katmanında Handler sınıflarında tüketiyor olacağız
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.GetReadRepository<Product>().GetAllAsync(include:p=>p.Include(p=>p.Brand));

            IList<GetAllProductsQueryResponse> response = new List<GetAllProductsQueryResponse>();

            //AutoMapper bu alttaki işlemi hallediyor

            foreach (var product in products)
            {
                response.Add(new GetAllProductsQueryResponse()
                {
                    Description = product.Description,
                    Discount = product.Discount,
                    Price = product.Price - (product.Price * product.Discount / 100),
                    Title = product.Title
                });
            }

            //var brand = mapper.Map<BrandDTO, Brand>(new Brand());

            //var mappedProducts = mapper.Map<GetAllProductsQueryResponse, Product>(products);



            foreach (var item in response)
                item.Price = item.Price - (item.Price * item.Discount / 100);


            //throw new BadRequestException("Kötü istek");
            return response;


        }
    }
}
