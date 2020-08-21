using System.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Van.Winkel.Financial.Infrastructure.EntityFramework;
using Van.Winkel.Financial.Infrastructure.Mediatr;
using Van.Winkel.Financial.Service.Customer;

namespace Van.Winkel.Financial.Host
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

            services.AddMediatR(typeof(GetCustomerRequest).Assembly);
            services.AddMediatRRequestValidators(typeof(GetCustomerRequest).Assembly);

            services.AddDbContext<IFinancialContext, FinancialContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("financial"))
                //options.UseInMemoryDatabase("InMemoryDbForTesting")
                );

            services.AddSpaStaticFiles(cfg =>
            {
                cfg.RootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseSpaStaticFiles(new StaticFileOptions { });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });
        }
    }
}
