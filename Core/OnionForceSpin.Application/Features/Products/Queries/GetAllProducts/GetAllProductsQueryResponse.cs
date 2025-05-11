using OnionForceSpin.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryResponse
    {
        //Bir response sınıfı bir  ViewModel veya DTO'yla aynı şey aslında
        //Response'da kullanıcıya döneceğimiz verileri gösteriyor olacağız.

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public BrandDTO Brand { get; set; }

    }
}
