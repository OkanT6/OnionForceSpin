using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionForceSpin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Faker faker = new Faker("tr");

                builder.Property(e => e.Price)
                      .HasPrecision(18, 2); // veya .HasColumnType("decimal(18,2)");

                builder.Property(e => e.Discount)
                      .HasPrecision(5, 2); // ihtiyacına göre ayarla
            

            Product product1 = new()
            {
                Id = 1,
                Title = "Elektrikli Süpürge Fakir",
                Description = "Güçlü emiş güçlü Fakir Elektrikli Süpürge",
                BrandId = 1,
                Discount = 10,
                Price = 500,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),
                IsDeleted = false


            };
            Product product2 = new()
            {
                Id = 2,
                Title = "Elbise Kırmızı",
                Description = "Özel gün düğün/nişan/balo elbisesi." +
                "Şık iddalı",
                BrandId = 2,
                Discount = 15,
                Price = 2500,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),
                IsDeleted = false


            };
            Product product3 = new()
            {
                Id = 3,
                Title = "Huawei D14 Laptop",
                Description = "Huawei D14 LAPTOP 16 GB RAM 516GB SSD",
                BrandId = 3,
                Discount = 25,
                Price = 30000,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),

                IsDeleted = true
            };
            builder.HasData(product1, product2, product3);
        }
    }
}
