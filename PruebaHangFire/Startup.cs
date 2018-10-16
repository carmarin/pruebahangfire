using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClasesGenericas;
using Hangfire;
using Hangfire.States;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PruebaHangFire
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHangfire(x => x.UseSqlServerStorage("Server=tcp:pruebaboilerplate.database.windows.net,1433;Initial Catalog=PruebaHangFire;Persist Security Info=False;User ID=boiler;Password=carmarmu*1234;MultipleActiveResultSets=False;"));
            
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
            app.UseHangfireDashboard();
            var client = new BackgroundJobClient();
            IState state = new EnqueuedState("prueba");
            DTOPrueba prueba = new DTOPrueba();
            client.Create<ITareaBase>(x => x.Ejecutar(prueba), state);

        }
        
    }
}
