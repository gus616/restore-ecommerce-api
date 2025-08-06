using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public string? ImageUrl { get; set; } = null;

        public bool isPrimary { get; set; } = false;

        //FK to Product

        public int ProductId { get; set; }

        public Product? Product { get; set; } = null;
    }
}
