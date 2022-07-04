using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Vacation_Planning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// ����������� ��������
        /// </summary>
        /// <param name="services">�������</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // ����������� JSON �������������
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // ����������� ������������
            services.AddControllers();
            // ����������� ��������� ���� ������
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<VacationPlanningContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("VacationPlanningContextProd")));
            else
                services.AddDbContext<VacationPlanningContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("VacationPlanningContext")));
            services.BuildServiceProvider().GetService<VacationPlanningContext>().Database.Migrate();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
    }
}
