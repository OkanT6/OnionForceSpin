using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Products.Commands.DeleteProducts
{
    public class DeleteProductCommandRequest:IRequest
    {
        public int Id { get; set; }
    }
}
