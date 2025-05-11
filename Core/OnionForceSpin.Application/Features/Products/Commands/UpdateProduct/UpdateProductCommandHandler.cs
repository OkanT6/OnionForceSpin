using MediatR;
using OnionForceSpin.Application.Features.Products.Queries.GetAllProducts;
using OnionForceSpin.Application.Interfaces.AutoMapper;
using OnionForceSpin.Application.Interfaces.UnitOfWorks;
using OnionForceSpin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.GetReadRepository<Product>()
                .GetAsync(p => p.Id == request.Id && p.IsDeleted == false);

            if (product == null)
            {
                throw new Exception("Product not found or already deleted");
            }

            //// 2️⃣  Manuel güncelleme işlemi:
            //product.Title = request.Title;
            //product.Description = request.Description;
            //product.Price = request.Price;
            //product.Discount = request.Discount;
            //product.BrandId = request.BrandId;



            // Burada `request` nesnesini `Product` nesnesine map etmemiz gerekiyor.
            //var mappedProduct= mapper.Map<Product,UpdateProductCommandRequest>(request);
            // 🔍 Mapper'ın düzgün çalışıp çalışmadığını anlamak için test edelim
            Console.WriteLine("BEFORE MAP => Title: " + product.Title + " | Description: " + product.Description);

            // 🔄 Mapper ile güncelleme
            //mapper.Map<UpdateProductCommandRequest, Product>(request, product);
            var mappedProduct= mapper.Map<Product,UpdateProductCommandRequest>(request);


            // 🔍 Mapping sonrası değerler kontrol ediliyor
            Console.WriteLine("AFTER MAP => Title: " + product.Title + " | Description: " + product.Description);



            var productCategories = await unitOfWork.GetReadRepository<ProductCategory>().GetAllAsync(p => p.ProductId == request.Id);

            await unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);

            foreach (var categoryId in request.CategoryIds)
            {
                await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory()
                {
                    CategoryId = categoryId,
                    ProductId = request.Id
                });
            }

            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(mappedProduct);
            await unitOfWork.SaveAsync();
        }
    }
}
