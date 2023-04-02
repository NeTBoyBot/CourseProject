using Doska.AppServices.IRepository;
using Doska.Domain;
using Doska.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doska.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly IBaseRepository<Order> _baseRepository;

        public OrderRepository(IBaseRepository<Order> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Order model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Order model)
        {
            await _baseRepository.DeleteAsync(model);
        }

        public async Task EditOrderAsync(Order edit)
        {
            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Order> FindById(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<Order> FindWhere(Expression<Func<Order, bool>> predicate, CancellationToken token)
        {
            var data = _baseRepository.GetAllFiltered(predicate);

            return await data.Where(predicate).FirstOrDefaultAsync(token);
        }

        public IQueryable<Order> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
