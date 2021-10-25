using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademyApp.Data.Context;
using AcademyApp.Infrastructure.Mapping;
using AcademyApp.Models;
using AcademyApp.Service;
using Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcademyApp
{
    public class Startup
    {

        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMapping();

            services.AddDbContext<AccountContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, AccountContext>();

            services.AddIdentity<UserIdentity, IdentityRole>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                        options.Password.RequiredLength = 4;
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.SignIn.RequireConfirmedEmail = false;
                        options.SignIn.RequireConfirmedPhoneNumber = false;
                    }
                )
                .AddEntityFrameworkStores<AccountContext>();


            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("Admin", policy => { policy.RequireRole("Admin"); });
                opts.AddPolicy("SuperAdmin", policy => { policy.RequireRole("SuperAdmin"); });
                opts.AddPolicy("Moderator", policy => { policy.RequireRole("Moderator"); });
                opts.AddPolicy("Basic", policy => { policy.RequireRole("Basic"); });
            });

            services.AddSingleton<ITokenManagement, TokenManagement>();
            services.AddScoped<IClientService, ClientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
