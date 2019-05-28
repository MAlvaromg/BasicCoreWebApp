using BasicCoreWebApp.DataAccess;
using BasicCoreWebApp.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using System;
using System.Net;

namespace BasicCoreWebApp
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwagger();
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddScoped<BasicCoreWebAppDbContext, BasicCoreWebAppDbContext>();

            var connection = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<BasicCoreWebAppDbContext>(options => options.UseSqlServer(connection));
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
            //This should be in a Middleware
            app.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (DomainException exception)
                {
                    //log exception
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync(exception.Message);
                }
                catch (Exception exception)
                {
                    //log exception
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync("There was an error, please try again later or contact the server administrator");
                }
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });
            app.UseMvc();
        }
    }
}
