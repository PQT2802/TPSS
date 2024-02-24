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
            services.AddTransient<IUserDetailRepository, UserDetailRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            //Service

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IReservationService, ReservationService>();
            
            //Exception Handler
            ////
            ///
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
