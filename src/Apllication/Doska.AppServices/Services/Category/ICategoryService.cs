using Doska.Contracts.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Category
{
    public interface ICategoryService
    {
        Task<InfoCategoryResponse> GetByIdAsync(Guid id);

        Task<Guid> CreateCategoryAsync(CreateOrEditCategoryRequest registerCategory);

        Task<IReadOnlyCollection<InfoCategoryResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id);

        Task<InfoCategoryResponse> EditCategoryAsync(Guid Id, CreateOrEditCategoryRequest editAd);
    }
}
