using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionForceSpin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Persistence.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(256);

            Faker faker = new Faker("tr");

            Brand brand1 = new()
            {
                Id = 1,
                Name = faker.Commerce.Department(),
            };

            Brand brand2 = new()
            {
                Id = 2,
                Name = faker.Commerce.Department(),
            };

            Brand brand3 = new()
            {
                Id = 3,
                Name = faker.Commerce.Department(),
                IsDeleted = true,
            };

            builder.HasData(brand1, brand2, brand3);
        }
    }
}
