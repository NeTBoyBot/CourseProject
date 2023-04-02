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
    public class ProductRepository : IProductRepository
    {
        public readonly IBaseRepository<Product> _baseRepository;

        public ProductRepository(IBaseRepository<Product> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Product model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Product model)
        {
            await _baseRepository.DeleteAsync(model);
        }

        public async Task EditProductAsync(Product edit)
        {
            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Product> FindById(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<Product> FindWhere(Expression<Func<Product, bool>> predicate, CancellationToken token)
        {
            var data = _baseRepository.GetAllFiltered(predicate);

            return await data.Where(predicate).FirstOrDefaultAsync(token);
        }

        public IQueryable<Product> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
