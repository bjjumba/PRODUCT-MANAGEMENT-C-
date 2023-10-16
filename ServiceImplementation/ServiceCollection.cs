using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection  RegisterServices( this IServiceCollection services)
        {
            services.AddTransient<IProductRepository,ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserLoginService , UserLogin>();
            return services;

        }
    }
}
