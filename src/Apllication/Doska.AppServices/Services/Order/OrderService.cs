using AutoMapper;
using Doska.AppServices.IRepository;
using Doska.Contracts.OrderDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.Order
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _OrderRepository;
        public IConfiguration _configuration;
        public readonly IMapper _mapper;

        public OrderService(IOrderRepository OrderRepository, IMapper mapper, IConfiguration conf)
        {
            _OrderRepository = OrderRepository;
            _mapper = mapper;
            _configuration = conf;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrEditOrderRequest registerOrder)
        {
            var newOrder = _mapper.Map<Domain.Order>(registerOrder);
            await _OrderRepository.AddAsync(newOrder);

            return newOrder.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingOrder = await _OrderRepository.FindById(id);
            await _OrderRepository.DeleteAsync(existingOrder);
        }

        public async Task<InfoOrderResponse> EditOrderAsync(Guid Id, CreateOrEditOrderRequest editOrder)
        {
            var existingOrder = await _OrderRepository.FindById(Id);
            await _OrderRepository.EditOrderAsync(_mapper.Map(editOrder, existingOrder));

            return _mapper.Map<InfoOrderResponse>(editOrder);
        }

        public async Task<IReadOnlyCollection<InfoOrderResponse>> GetAll(int take, int skip)
        {
            return await _OrderRepository.GetAll()
                .Select(a => new InfoOrderResponse
                {
                    Id = a.Id,
                    ProductId = a.ProductId,
                    UserId = a.UserId,
                    Product = new Contracts.ProductDto.InfoProductResponse
                    {
                        CategoryId = a.Product.CategoryId,
                        Id = a.Product.Id,
                        Name = a.Product.Name,
                        Price = a.Product.Price
                    }
                    
                }).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<InfoOrderResponse> GetByIdAsync(Guid id)
        {
            var existingOrder = await _OrderRepository.FindById(id);
            return _mapper.Map<InfoOrderResponse>(existingOrder);
        }

        public async Task<IReadOnlyCollection<InfoOrderResponse>> GetUserOrders(int take, int skip,Guid UserId)
        {
            return await _OrderRepository.GetAll()
                .Select(a => new InfoOrderResponse
                {
                    Id = a.Id,
                    ProductId = a.ProductId,
                    UserId = a.UserId,
                    Product = new Contracts.ProductDto.InfoProductResponse
                    {
                        Category = new Contracts.CategoryDto.InfoCategoryResponse
                        {
                            Id = a.Product.Category.Id,
                            Name = a.Product.Category.Name
                        },
                        CategoryId = a.Product.CategoryId,
                        Id = a.Product.Id,
                        Name = a.Product.Name,
                        Price = a.Product.Price
                    }

                }).Where(o=>o.UserId == UserId).OrderBy(a => a.Id).Skip(skip).Take(take).ToListAsync();
        }
    }
}
