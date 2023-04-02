using Doska.Contracts.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Product
{
    public interface IProductService
    {
        Task<InfoProductResponse> GetByIdAsync(Guid id);

        Task<Guid> CreateProductAsync(CreateOrEditProductRequest registerProduct);

        Task<IReadOnlyCollection<InfoProductResponse>> GetAll(int take, int skip);

        Task DeleteAsync(Guid id);

        Task<InfoProductResponse> EditProductAsync(Guid Id, CreateOrEditProductRequest editAd);
    }
}
