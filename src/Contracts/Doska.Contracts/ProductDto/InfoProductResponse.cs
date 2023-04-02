using Doska.Contracts.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.Contracts.ProductDto
{
    public class InfoProductResponse
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public InfoCategoryResponse Category { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
