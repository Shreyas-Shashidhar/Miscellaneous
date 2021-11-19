using Battleship.Api.Application.Interfaces;
using Battleship.Api.Common;
using Battleship.Api.Middleware;
using Battleship.Api.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Battleship.Api
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
            // Appplication 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            // Persistence
            services.AddDbContext<BattleshipDbContext>(options => options.UseInMemoryDatabase("BattleshipApiTestDatabase"));
            services.AddScoped<IBattleshipDbContext>(provider => provider.GetService<BattleshipDbContext>());

            services.AddControllers().
                AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IBattleshipDbContext>());

            services.AddHealthChecks();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Battleship API");
                c.RoutePrefix = string.Empty;
            });

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseRouting();

            app.UseCors(policyBuilder =>
            {
                policyBuilder
                    .WithOrigins("http://localhost:52890")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
