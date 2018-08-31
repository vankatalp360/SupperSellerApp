using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperSeller.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperSeller.Models;
using SuperSeller.Services.Admin;
using SuperSeller.Services.Admin.Interfaces;
using SuperSeller.Services.Users;
using SuperSeller.Services.Users.Interfaces;
using SuperSeller.Web.Areas.Identity.Services;
using SuperSeller.Web.Common;

namespace SuperSeller.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication()
                .AddFacebook(option =>
                {
                    option.AppId = Configuration.GetSection("ExternalAuthentication:Facebook:AppId").Value;
                    option.AppSecret = Configuration.GetSection("ExternalAuthentication:Facebook:AppSecret").Value;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration.GetSection("ExternalAuthentication:Google:ClientId").Value;
                    options.ClientSecret = Configuration.GetSection("ExternalAuthentication:Google:ClientSecret").Value;
                });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 6,
                    RequiredUniqueChars = 1,
                    RequireLowercase = true,
                    RequireDigit = false,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = false
                };

                // options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddSingleton<IEmailSender, SendGridEmailSender>();
            services.Configure<SendGridOptions>(Configuration.GetSection("EmailSettings"));

            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminPagesPolicy", builder => builder.RequireRole("Administrator").Build());
            });

            services.AddAutoMapper();

            RegisterServiceLayer(services);

            services
                .AddMvc(option =>
                {
                    option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .AddRazorPagesOptions(option =>
                {
                    option.Conventions.AuthorizeAreaFolder("Admin", "/", "AdminPagesPolicy");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.SeedDatabase();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        

        private static void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IConditionService, ConditionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAdService, AdService>();
        }
    }
}
