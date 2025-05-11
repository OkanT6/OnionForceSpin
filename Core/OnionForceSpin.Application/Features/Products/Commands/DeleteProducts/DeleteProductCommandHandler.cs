using MediatR;
using OnionForceSpin.Application.Interfaces.UnitOfWorks;
using OnionForceSpin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Products.Commands.DeleteProducts
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product= await unitOfWork.GetReadRepository<Product>().GetAsync(p=>p.Id==request.Id && p.IsDeleted==false);
            product.IsDeleted = true;
            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);

            await unitOfWork.SaveAsync();
        }
    }
}
