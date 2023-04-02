using Doska.Contracts.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Order
{
    public interface IOrderService
    {
        Task<InfoOrderResponse> GetByIdAsync(Guid id);

        Task<Guid> CreateOrderAsync(CreateOrEditOrderRequest registerOrder);

        Task<IReadOnlyCollection<InfoOrderResponse>> GetAll(int take, int skip);

        Task<IReadOnlyCollection<InfoOrderResponse>> GetUserOrders(int take, int skip,Guid UserId);

        Task DeleteAsync(Guid id);

        Task<InfoOrderResponse> EditOrderAsync(Guid Id, CreateOrEditOrderRequest editAd);
    }
}
