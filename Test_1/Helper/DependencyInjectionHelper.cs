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
            //Service

            services.AddTransient<IUserService, UserService>();
            //services.AddTransient<IUserService, UserService2>();


            //Exception Handler


            ////
            ///
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
