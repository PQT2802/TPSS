﻿using Microsoft.EntityFrameworkCore.Metadata;
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

            services.AddTransient<IPropertyRepository, PropertyRepository>();

            services.AddTransient<IUserDetailRepository, UserDetailRepository>();


            //Service

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IImageService, ImageService>();
            

            //services.AddTransient<IUserService, UserService2>();



            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            
            //Exception Handler
            ////
            ///
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
