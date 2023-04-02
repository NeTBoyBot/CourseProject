using Doska.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> FindWhere(Expression<Func<Order, bool>> predicate, CancellationToken token);

        Task<Order> FindById(Guid id);

        IQueryable<Order> GetAll();

        public Task AddAsync(Order model);

        Task DeleteAsync(Order model);

        Task EditOrderAsync(Order edit);
    }
}
