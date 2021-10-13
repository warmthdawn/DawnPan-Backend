using DawnPan.Entity;
using DawnPan.Service;
using DawnPan.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DawnPan
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
            services.AddControllers();
            services.AddDbContext<AppDbContext>(b =>
            {
                var connectionString = "server=localhost;user=root;password=12345678;database=dawnpan";
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
                b.UseMySql(connectionString, serverVersion)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();


            });

            services.AddScoped(_ => new FileUtils(Configuration["FileSettings:SavePath"]));
            services.AddScoped<FileService>();
            services.AddScoped<UploadService>();
            services.AddScoped<DirectoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder
                .WithOrigins("http://127.0.0.1:3000", "http://localhost:3000")
                .AllowAnyHeader()//Ensures that the policy allows any header.
                .AllowAnyMethod();
            });

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
