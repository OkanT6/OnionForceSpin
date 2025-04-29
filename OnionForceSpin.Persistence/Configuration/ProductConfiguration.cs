using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionForceSpin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            Faker faker = new Faker("tr");

            Product product1 = new()
            {
                Id = 1,
                Title = faker.Commerce.Product(),
                Description = faker.Commerce.ProductDescription(),
                BrandId = 1,
                Discount = faker.Random.Decimal(5, 20),
                Price = faker.Random.Decimal(50, 500)
            };
            Product product2 = new()
            {
                Id = 2,
                Title = faker.Commerce.Product(),
                Description = faker.Commerce.ProductDescription(),
                BrandId = 2,
                Discount = faker.Random.Decimal(5, 20),
                Price = faker.Random.Decimal(50, 500)
            };
            Product product3 = new()
            {
                Id = 3,
                Title = faker.Commerce.Product(),
                Description = faker.Commerce.ProductDescription(),
                BrandId = 3,
                Discount = faker.Random.Decimal(5, 20),
                Price = faker.Random.Decimal(50, 500),
                IsDeleted = true
            };
            builder.HasData(product1, product2, product3);
        }
    }
}
