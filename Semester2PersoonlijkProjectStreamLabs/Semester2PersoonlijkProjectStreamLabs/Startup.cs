using Data.Contexts;
using Data.Interfaces;
using Logic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Semester2PersoonlijkProjectStreamLabs
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Home/Forbidden";
                    options.LoginPath = "/User/Login";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", p => p.RequireAuthenticatedUser().RequireRole("Admin"));
                options.AddPolicy("Viewer", p => p.RequireAuthenticatedUser().RequireRole("Viewer"));
            });

            services.AddTransient(_ => new Data.Connection(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICategoryContext, CategoryContextSQL>();
            services.AddScoped<ICommentContext, CommentContextSQL>();
            services.AddScoped<IUserContext, UserContextSQL>();
            services.AddScoped<IVideoContext, VideoContextSQL>();
            services.AddScoped<IAccountContext, AccountContext>();
            services.AddScoped<IReportContext, ReportContext>();

            services.AddScoped<CommentLogic>();
            services.AddScoped<AccountLogic>();
            services.AddScoped<CategoryLogic>();
            services.AddScoped<ReportLogic>();
            services.AddScoped<UserLogic>();
            services.AddScoped<VideoLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Viewer",
                    template: "{controller=Viewer}/{action=Index}/{id?}");
            });
        }
    }
}
