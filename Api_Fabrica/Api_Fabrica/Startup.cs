using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Fabrica.Context;
using Api_Fabrica.Services.implemaentation;
using Api_Fabrica.Services.interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Api_Fabrica
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
            services.AddCors(options =>
            {
                options.AddPolicy(name: "public",
                    builder =>
                    {
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();

                    });
            });
            services.AddControllers();
            AddSwagger(services);


            services.AddDbContext<MyDbContext>(item =>
            item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrdersService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fabrica API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Fabrica {groupName}",
                    Version = groupName,
                    Description = "Fabrica API",
                    Contact = new OpenApiContact
                    {
                        Name = "Fabrica Company",
                        Email = string.Empty,
                        Url = new Uri("https://Fabrica.com/"),
                    }
                });
            });
        }
    }
}
