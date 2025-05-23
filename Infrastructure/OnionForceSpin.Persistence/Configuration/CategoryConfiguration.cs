﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionForceSpin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Category category1 = new()
            {
                Id = 1,
                Name = "Elektrik",
                Priority = 1,
                ParentId=0,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),
                IsDeleted = false


            };

            Category category2 = new()
            {
                Id = 2,
                Name = "Moda",
                Priority = 2,
                ParentId = 0,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),
                IsDeleted = false


            };


            Category parentCategory1 = new()
            {
                Id = 3,
                Name = "Bilgisayar",
                Priority = 1,
                ParentId = 1,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),
                IsDeleted = false


            };

            Category parentCategory2 = new()
            {
                Id = 4,
                Name = "Kadın",
                Priority = 1,
                ParentId = 2,
                CreatedDate = new DateTime(2025, 4, 30, 18, 45, 12),
                IsDeleted = false


            };

            builder.HasData(category1, category2,parentCategory1,parentCategory2);
        }
    }
}
