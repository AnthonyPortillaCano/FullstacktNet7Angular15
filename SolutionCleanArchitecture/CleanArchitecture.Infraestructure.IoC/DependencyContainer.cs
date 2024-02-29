using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infraestructure.Data.Context;
using CleanArchitecture.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infraestructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services,IConfiguration configuration)
        {
           // services.AddTransient<IProductServices, ProductServices>();

            //services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork>(options => new UnitOfWork(new BdempresaContext(new DbContextOptionsBuilder<BdempresaContext>().UseSqlServer(configuration.GetConnectionString("ConnectionSqlServer")).Options), configuration.GetConnectionString("ConnectionSqlServer")));
        }
    }
}
