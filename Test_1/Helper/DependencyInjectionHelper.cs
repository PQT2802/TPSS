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

            services.AddTransient<IProjectRepository, ProjectRepository>();


            //Service

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IProjectService, ProjectService>();

            //services.AddTransient<IUserService, UserService2>();

            return services;
        }
    }
}
