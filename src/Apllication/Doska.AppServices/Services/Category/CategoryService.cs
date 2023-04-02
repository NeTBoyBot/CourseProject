using AutoMapper;
using Doska.AppServices.IRepository;
using Doska.Contracts.CategoryDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Category
{
    public class CategoryService : ICategoryService
    {
        public readonly ICategoryRepository _CategoryRepository;
        public IConfiguration _configuration;
        public readonly IMapper _mapper;

        public CategoryService(ICategoryRepository CategoryRepository, IMapper mapper, IConfiguration conf)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
            _configuration = conf;
        }

        public async Task<Guid> CreateCategoryAsync(CreateOrEditCategoryRequest registerCategory)
        {
            var newCategory = _mapper.Map<Domain.Category>(registerCategory);
            await _CategoryRepository.AddAsync(newCategory);

            return newCategory.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingCategory = await _CategoryRepository.FindById(id);
            await _CategoryRepository.DeleteAsync(existingCategory);
        }

        public async Task<InfoCategoryResponse> EditCategoryAsync(Guid Id, CreateOrEditCategoryRequest editCategory)
        {
            var existingCategory = await _CategoryRepository.FindById(Id);
            await _CategoryRepository.EditCategoryAsync(_mapper.Map(editCategory, existingCategory));

            return _mapper.Map<InfoCategoryResponse>(editCategory);
        }

        public async Task<IReadOnlyCollection<InfoCategoryResponse>> GetAll(int take, int skip)
        {
            return await _CategoryRepository.GetAll()
                .Select(a => new InfoCategoryResponse
                {
                    Id = a.Id,
                    Name = a.Name
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<InfoCategoryResponse> GetByIdAsync(Guid id)
        {
            var existingCategory = await _CategoryRepository.FindById(id);
            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }
    }
}
