using Doska.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.IRepository
{
    public interface ICategoryRepository
    {
        Task<Category> FindWhere(Expression<Func<Category, bool>> predicate, CancellationToken token);

        Task<Category> FindById(Guid id);

        IQueryable<Category> GetAll();

        public Task AddAsync(Category model);

        Task DeleteAsync(Category model);

        Task EditCategoryAsync(Category edit);
    }
}
