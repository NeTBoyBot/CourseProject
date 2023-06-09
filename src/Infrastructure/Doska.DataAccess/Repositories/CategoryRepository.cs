﻿using Doska.AppServices.IRepository;
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
    public class CategoryRepository : ICategoryRepository
    {
        public readonly IBaseRepository<Category> _baseRepository;

        public CategoryRepository(IBaseRepository<Category> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task AddAsync(Category model)
        {
            return _baseRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Category model)
        {
            await _baseRepository.DeleteAsync(model);
        }

        public async Task EditCategoryAsync(Category edit)
        {
            await _baseRepository.UpdateAsync(edit);
        }

        public async Task<Category> FindById(Guid id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<Category> FindWhere(Expression<Func<Category, bool>> predicate, CancellationToken token)
        {
            var data = _baseRepository.GetAllFiltered(predicate);

            return await data.Where(predicate).FirstOrDefaultAsync(token);
        }

        public IQueryable<Category> GetAll()
        {
            return _baseRepository.GetAll();
        }
    }
}
