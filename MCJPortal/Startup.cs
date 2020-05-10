using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using MCJPortal.Application.Services;
using MCJPortal.Application.Services.Interfaces;
using MCJPortal.Domain;
using MCJPortal.Domain.Context;
using MCJPortal.Domain.Models.Authorization;
using MCJPortal.Middleware;
using MCJPortal.ViewModels.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace MCJPortal
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
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/";
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 
            services.AddSwaggerDocument();

            // configure automapper
            var automapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfiles(new List<Profile> {
                    new ListItemProfile(),
                    new UserProfile(),
                    new ProjectProfile(),
                    new CountryProfile(),
                    new ProjectLineProfile()
                })
            );
            IMapper mapper = automapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            #region services
            services.AddScoped<IProjectsService, ProjectsService>(); 
            services.AddScoped<IListItemsService, ListItemsService>(); 
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IRolePermissionsService, RolePermissionsService>();
            services.AddScoped<IProjectLineService, ProjectLineService>();
            #endregion

            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MCJPortal.Domain"));
                options.UseOpenIddict();
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddDefaultTokenProviders();

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            // Register the OpenIddict services.
            // Note: use the generic overload if you need
            // to replace the default OpenIddict entities.
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<MainDbContext>();
                })
                .AddServer(options =>
                {
                    options.UseMvc();
                    // Enable the authorization and token endpoints (required to use the code flow).
                    options.EnableTokenEndpoint("/api/auth/connect");

                    options.AllowPasswordFlow();
                    options.AllowRefreshTokenFlow();

                    // Accept anonymous clients (i.e clients that don't send a client_id).
                    options.AcceptAnonymousClients();

                    //#if DEBUG
                    options.DisableHttpsRequirement();
                    //#endif
                    options.RegisterScopes(
                        OpenIdConnectConstants.Scopes.OpenId,
                        OpenIdConnectConstants.Scopes.Email,
                        OpenIdConnectConstants.Scopes.Phone,
                        OpenIdConnectConstants.Scopes.Profile,
                        OpenIdConnectConstants.Scopes.OfflineAccess
                    );

                    options.SetAccessTokenLifetime(TimeSpan.FromDays(30));
                })
                .AddValidation();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseMiddleware(typeof(ErrorHandler));
            

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
