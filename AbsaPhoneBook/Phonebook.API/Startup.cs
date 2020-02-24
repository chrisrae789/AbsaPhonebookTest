using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistance;
using Persistance.Context;
using Persistance.Interfaces;

namespace Phonebook.API
{
    public class Startup
    {
        readonly string ClientOrigin = "_ClientOrigin";

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
                options.AddPolicy(ClientOrigin,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4224")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddDbContextPool<AbsaPhonebookContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AbsaPhonebook"));
            });

            services.AddScoped<IPhonebookRepository, PhonebookRepository>();
            services.AddScoped<IPhonebookEntryRepository, PhonebookEntryRepository>();

            services.AddControllers();
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
                app.UseDeveloperExceptionPage();
            }
         
            app.UseCors(ClientOrigin);
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials();
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
        
            });
            app.UseDefaultFiles();
            

        }
    }
}
