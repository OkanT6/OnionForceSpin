using OnionForceSpin.Domain.Common;
using System;
using System.Collections.Generic;

namespace OnionForceSpin.Domain.Entities
{
    public class Category:EntityBase
    {
        // ➡️ 1️⃣ Fields (Private Değişkenler)
        private int _internalId;

        // ➡️ 2️⃣ Properties (Özellikler)
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

        // ➡️ 3️⃣ Constructors (Yapıcı Metotlar)
        public Category()
        {
            Details = new List<Detail>();
            ProductCategories = new List<ProductCategory>();
            //Console.WriteLine("Parametresiz constructor çalıştı.");
        }

        public Category(int parentId, string name, int priority) : this()
        {
            ParentId = parentId;
            Name = name;
            Priority = priority;
            Console.WriteLine("Parametreli constructor çalıştı.");
        }

        //// ➡️ 4️⃣ Methods (Metotlar)
        //public void PrintInfo()
        //{
        //    Console.WriteLine($"Category: {Name}, Priority: {Priority}");
        //}
    }
}
