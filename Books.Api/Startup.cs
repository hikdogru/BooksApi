using Books.Business.Abstract;
using Books.Business.Concrete;
using Books.Data.Abstract;
using Books.Data.Concrete.Ef;
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
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Books.Api
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
            // DbContext
            services.AddDbContext<BookContext>(option => option.UseSqlServer(Configuration.GetConnectionString("BookConnectionAzure")));

            // Service layer
            services.AddScoped<IBookService, BookManager>();
            // Data layer
            services.AddScoped<IBookRepository, EfBookRepository>();

            // Json Patch Update
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                // Json formatter
                x.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });


            services.AddControllers();
            // XML
            services.AddControllers().AddXmlSerializerFormatters();

            // Automapping
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Books.Api", Version = "v1" });
            //});

            services.AddControllersWithViews();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
