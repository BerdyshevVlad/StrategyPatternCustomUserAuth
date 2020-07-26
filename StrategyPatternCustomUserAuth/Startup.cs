using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StrategyPatternCustomUserAuth.Entities.Interfaces;
using StrategyPatternCustomUserAuth.Services.AuthHeader;
using StrategyPatternCustomUserAuth.Services.AuthHeader.Interfaces;
using StrategyPatternCustomUserAuth.Services.RequestMessageAccessor;
using StrategyPatternCustomUserAuth.Services.RequestUserInfoService;
using StrategyPatternCustomUserAuth.Services.RequestUserInfoService.Interfaces;
using StrategyPatternCustomUserAuth.Services.RequestUserInfoWrapper;
using StrategyPatternCustomUserAuth.Services.UserMainInfoService;

namespace StrategyPatternCustomUserAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            ServiceProvider = serviceProvider;
        }
        public IServiceProvider ServiceProvider { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<RequestMessageAccessor>();
            RegisterSecurityModule(services);
            services.AddScoped<UserMainInfoService>();
            services.AddScoped<RequestUserInfoWrapper>((provider) =>
            {
               var userService = provider.GetService<UserMainInfoService>();
               var authHeader = provider.GetService<IAuthHeader>();

               if (authHeader.IsHeaderProvided)
               {
                   return new RequestUserInfoWrapper(
                       () => userService.GetUserAsync(authHeader.Content)
                       );
               }
               return new RequestUserInfoWrapper(
                    () => Task.FromResult<IRequestUserInfo>(null));
            });

            services.AddScoped<IRequestUserInfoService, RequestUserInfoService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

        }

        public void RegisterSecurityModule(IServiceCollection services)
        {
            services.AddScoped<IAuthHeader>((provider) =>
            {
                var message = provider.GetService<RequestMessageAccessor>().CurrentMessageNullable;

                if (message != null)
                {
                    // Если явно указали в swagger ui
                    var authorizationHeaderValue = GetAuthHeader(message, "Authorization");
                    if (authorizationHeaderValue != null)
                    {
                        return authorizationHeaderValue;
                    }
                }
                return new AuthHeader(null);
            });
        }

        private static AuthHeader GetAuthHeader(HttpRequest message, string headerName)
        {
            var parseResult = message.Headers.TryGetValue(headerName, out var authHeaderValue);

            if (parseResult)
            {
                var headerValue = authHeaderValue.ToArray();

                if (headerValue.Any())
                {
                    {
                        return new AuthHeader(headerValue.First());
                    }
                }
            }

            return null;
        }

    }
}
