using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandRequest:IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }


        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int BrandId { get; set; }

        public List<int > CategoryIds { get; set; }

    }
}
