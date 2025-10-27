using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace alamana.Infrastructure.Identity
{
    public static class DependencyInjection
    {
        //public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
        //{
        //    services
        //        .AddIdentityCore<AppUser>(opt =>
        //        {
        //            opt.Password.RequireDigit = true;
        //            opt.Password.RequiredLength = 8;
        //            opt.Password.RequireLowercase = true;
        //            opt.Password.RequireUppercase = true;
        //            opt.Password.RequireNonAlphanumeric = true;
        //            opt.Password.RequiredUniqueChars = 1;
        //            opt.User.RequireUniqueEmail = true;
        //        })
        //        .AddRoles<AppRole>()
        //        .AddEntityFrameworkStores<AlamanaDbContext>()
        //        .AddDefaultTokenProviders();

        //    // لو الامتداد AddSignInManager مش ظاهر، سجّله يدويًا:
        //    services.AddScoped<SignInManager<AppUser>>();

        //    return services;
        //}



        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            //services.AddDbContext<AlamanaDbContext>(opts =>
            //    opts.UseSqlServer(config.GetConnectionString("Default")));

            // مهم لTokenProviders (Reset/Email Confirm)
            services.AddDataProtection();

            // مهم لـ SignInManager و CurrentUser
            services.AddHttpContextAccessor();

            services
                .AddIdentityCore<AppUser>(opt =>
                {
                    opt.Password.RequireDigit = true;
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireNonAlphanumeric = true;
                    opt.Password.RequiredUniqueChars = 1;
                    opt.User.RequireUniqueEmail = true;
                })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AlamanaDbContext>()
                .AddDefaultTokenProviders();

            // لو ما كنتش تستخدم AddIdentity()؛ ضيف الـ SignInManager يدويًا
            services.AddScoped<SignInManager<AppUser>>();

            return services;
        }

    }
}
