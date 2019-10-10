using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using PGS.Comman;
using PGS.ServicesContract;
using PGS.WeatherSerices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PGS.WebApi.MiddleWare;

namespace PGS.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// startup constructor IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuraion compile time
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            var weatherfilepath = Configuration.GetSection("WeatherData");
            services.Configure<WeatherData>(weatherfilepath);
            services.AddScoped(typeof(IWeatherService), typeof(WeatherService));
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });
            services.AddResponseCompression();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Weather API", Version = "v1" });
                    c.OperationFilter<HeaderParameter>();
                    c.IncludeXmlComments(AppContext.BaseDirectory + @"/PGS.WebApi.xml");
                });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizationTokenFilter());
            }).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

           

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

        }

        /// <summary>
        /// configuraion run time   
        /// </summary>
        /// <param name="app"></param>  
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API V1");
                c.RoutePrefix = string.Empty;

            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMvc();
        }
    }
}
