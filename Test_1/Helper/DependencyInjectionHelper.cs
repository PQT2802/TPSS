using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
using TPSS.Business.Service;
using TPSS.Business.Service.Impl;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;

namespace TPSS.API.Helper
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            //Repository
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IReservationRepository, ReservationRepository>();

            services.AddTransient<IContractRepository, ContractRepository>();


            //Service
            
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IReservationService, ReservationService>();

            services.AddTransient<IContractService, ContractService>();

            //services.AddTransient<IContractService, ContractService>();

            //services.AddTransient<IUserService, UserService2>();

            return services;
        }
    }
}
