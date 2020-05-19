using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
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

namespace Api
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
            services.AddCors();
            services.AddControllers();
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Service.Validators.AutorValidator>());
            services.AddTransient<IValidator<Domain.Entities.Autor>, Service.Validators.AutorValidator>();

            services.AddDbContext<Infra.Data.Context.SQLContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"))
            );

            //// Register the Swagger generator, defining 1 or more Swagger documents
            ConfigureSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            //// Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            //// specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste Francisco API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Teste Francisco API", Version = "v1" });
                x.AddSecurityDefinition("Bearer",
                                        new OpenApiSecurityScheme
                                        {
                                            In = ParameterLocation.Header,
                                            Description = "Para autenticação digite a palavra 'Bearer' seguido por espaço e o JWT Token",
                                            Name = "Authorization",
                                            Type = SecuritySchemeType.ApiKey
                                        });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                                                                            {
                                                                                new OpenApiSecurityScheme
                                                                                {
                                                                                Reference = new OpenApiReference
                                                                                    {
                                                                                    Type = ReferenceType.SecurityScheme,
                                                                                    Id = "Bearer"
                                                                                    },
                                                                                    Scheme = "oauth2",
                                                                                    Name = "Bearer",
                                                                                    In = ParameterLocation.Header,

                                                                                },
                                                                                new List<string>()
                                                                            }
                });
            });
        }
    }
}
