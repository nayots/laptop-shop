using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nayots.LaptopShop.Common.Contracts.Data;
using Nayots.LaptopShop.Host.Extensions;
using System;

namespace Nayots.LaptopShop.Host
{
    public class Startup
    {
        private const string _corsPolicyName = "AllowAll";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddJWTAuth(Configuration);
            services.AddServices();
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSwagger();
            services.AddDB(Configuration);
            services.AddValidators();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nayots.LaptopShop.Host v1"));

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            serviceProvider.GetService<IDataBoostrap>().Setup();
        }
    }
}