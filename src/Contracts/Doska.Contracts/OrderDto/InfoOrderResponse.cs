using Doska.Contracts.ProductDto;
using Doska.Contracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.Contracts.OrderDto
{
    public class InfoOrderResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public InfoUserResponse User { get; set; }

        public Guid ProductId { get; set; }

        public InfoProductResponse Product { get; set; }
    }
}
