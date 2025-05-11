using OnionForceSpin.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Domain.Entities
{
    public class Product : EntityBase
    {
        
        public string Title { get; set; }
        public string Description { get; set; }


        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        //public required string ImagePath { get; set; }   

        public Product()
        {
            ProductCategories = new List<ProductCategory>();
        }

        public Product(string title,string description,int brandId, decimal price, decimal discount):this()
        {
            Title = title;
            Description = description;
            BrandId = brandId;
            Price = price;
            Discount = discount;
        }

    }
}
