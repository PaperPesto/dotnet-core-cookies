using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApplication6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            string valuefromcommandline = configuration.GetValue<string>("testkey");
            string valuefromsetting = configuration.GetValue<string>("database:connectionString");

            Console.WriteLine("==== Configurazione passata da command line:" + valuefromcommandline);
            Console.WriteLine("==== String di connessione da configurazione:" + valuefromsetting);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<ITransientSalutante, Salutatore>();
            services.AddScoped<IScopedSalutante, Salutatore>();
            services.AddSingleton<ISingletonSalutante, Salutatore>();
            services.AddTransient<IService, Service>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
