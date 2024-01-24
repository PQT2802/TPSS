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
<<<<<<< HEAD
            services.AddTransient<IProjectRepository, ProjectRepository>();   
=======
>>>>>>> DEV_THANG

            //Service
            
            services.AddTransient<IUserService, UserService>();
<<<<<<< HEAD
            services.AddTransient<IProjectService, ProjectService>();
=======
            //services.AddTransient<IUserService, UserService2>();

>>>>>>> DEV_THANG



            ////
            return services;
        }
    }
}
