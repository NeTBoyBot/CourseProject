using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.Contracts.OrderDto
{
    public class CreateOrEditOrderRequest
    {
        public Guid UserId { get; set; }


        public Guid ProductId { get; set; }

    }
}
