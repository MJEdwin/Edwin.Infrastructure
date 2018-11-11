using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Edwin.Infrastructure.EntityFramework;
using Edwin.TestApi.Context;
using Microsoft.EntityFrameworkCore;
using Edwin.TestApi.Services;
using Swashbuckle.AspNetCore.Swagger;
using Edwin.Infrastructure.Castle;
using Edwin.Infrastructure.Domain.Event;

namespace Edwin.TestApi
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
            services.AddEntityFrameworkPool<TestContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("MySql")));

            services.AddScoped<InterceptorService>();

            services.AddScopedWithIntercrpt<IUserService, UserService>(typeof(InterceptorService));

            services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }));

            services.AddEventBus();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            app.UseMvc();
        }
    }
}
