using AutoMapper;
using Doska.AppServices.IRepository;
using Doska.Contracts.ProductDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Product
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _ProductRepository;
        public IConfiguration _configuration;
        public readonly IMapper _mapper;

        public ProductService(IProductRepository ProductRepository, IMapper mapper, IConfiguration conf)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
            _configuration = conf;
        }

        public async Task<Guid> CreateProductAsync(CreateOrEditProductRequest registerProduct)
        {
            var newProduct = _mapper.Map<Domain.Product>(registerProduct);
            newProduct.CategoryId = registerProduct.CategoryId;
            await _ProductRepository.AddAsync(newProduct);

            return newProduct.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingProduct = await _ProductRepository.FindById(id);
            await _ProductRepository.DeleteAsync(existingProduct);
        }

        public async Task<InfoProductResponse> EditProductAsync(Guid Id, CreateOrEditProductRequest editProduct)
        {
            var existingProduct = await _ProductRepository.FindById(Id);
            await _ProductRepository.EditProductAsync(_mapper.Map(editProduct, existingProduct));

            return _mapper.Map<InfoProductResponse>(editProduct);
        }

        public async Task<IReadOnlyCollection<InfoProductResponse>> GetAll(int take, int skip)
        {
            return await _ProductRepository.GetAll()
                .Select(a => new InfoProductResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Category = new Contracts.CategoryDto.InfoCategoryResponse
                    {
                        Id = a.Category.Id,
                        Name = a.Category.Name
                    }
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoProductResponse> GetByIdAsync(Guid id)
        {
            var existingProduct = await _ProductRepository.FindById(id);
            return _mapper.Map<InfoProductResponse>(existingProduct);
        }

    }
}
