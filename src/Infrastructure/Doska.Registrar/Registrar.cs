using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Doska.DataAccess.DataBase;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Doska.AppServices.MapProfile;
using Doska.AppServices.IRepository;

using Doska.DataAccess.Repositories;
using Doska.Infrastructure.BaseRepository;

using Doska.AppServices.Services.User;

using Doska.Infrastructure.Identity;
using Doska.AppServices.Services.Order;
using Doska.AppServices.Services.Category;
using Doska.AppServices.Services.Product;

namespace Doska.Registrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddCors();

            services.AddSingleton<IDbContextOptionsConfigurator<DoskaContext>, DoskaContextConfiguration>();

            services.AddDbContext<DoskaContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<DoskaContext>>()
                .Configure((DbContextOptionsBuilder<DoskaContext>)dbOptions)));

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<DoskaContext>()));

            services.AddAutoMapper(typeof(UserMapProfile),typeof(ProductMapProfile),
                typeof(CategoryMapProfile),typeof(OrderMapProfile));

            // Регистрация объявления
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddScoped<IClaimAcessor, HttpContextClaimAcessor>();

            return services;
        }
    }
}
