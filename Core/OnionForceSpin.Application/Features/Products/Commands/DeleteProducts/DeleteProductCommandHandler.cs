using MediatR;
using OnionForceSpin.Application.Features.Products.Commands.DeleteProducts;
using OnionForceSpin.Application.Interfaces.UnitOfWorks;
using OnionForceSpin.Domain.Entities;
using OnionForceSpin.Application.Exceptions.DefinedExceptions;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Unit>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.GetReadRepository<Product>().GetAsync(p => p.Id == request.Id);

        if (product == null)
        {
            Console.WriteLine("❌ Product bulunamadı, NotFoundException fırlatılıyor.");
            throw new NotFoundException($"Product with Id {request.Id} not found.");
        }

        product.IsDeleted = true;
        await unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
        await unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
