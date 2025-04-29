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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Category category1 = new()
            {
                Id = 1,
                Name = "Elektrik",
                Priorty = 1,
                ParentId=0,
            };

            Category category2 = new()
            {
                Id = 2,
                Name = "Moda",
                Priorty = 2,
                ParentId = 0,
            };


            Category parentCategory1 = new()
            {
                Id = 3,
                Name = "Bilgisayar",
                Priorty = 1,
                ParentId = 1,
            };

            Category parentCategory2 = new()
            {
                Id = 4,
                Name = "Kadın",
                Priorty = 1,
                ParentId = 2,
            };

            builder.HasData(category1, category2,parentCategory1,parentCategory2);
        }
    }
}
