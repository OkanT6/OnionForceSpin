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
    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            //Faker faker = new Faker("tr");

            Detail detail1 = new()
            {
                Id = 1,
                Title = "Volt",
                Description = "150V",
                CategoryId=1,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),
                IsDeleted = false


            };
            Detail detail2 = new()
            {
                Id = 2,
                Title = "SIZE",
                Description = "M",
                CategoryId = 2,
                IsDeleted = true,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12)

            };
            Detail detail3 = new()
            {
                Id = 3,
                Title = "RAM",
                Description = "16 GB",
                CategoryId = 3,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),
                IsDeleted = false


            };

            builder.HasData(detail1, detail2, detail3);
        }
    }
}
