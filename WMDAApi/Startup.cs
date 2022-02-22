using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WMDAApi.Models;
using WMDAApi.Services;
using WMDAApi.Utils;

namespace WMDAApi
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson().ConfigureApiBehaviorOptions( options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WMDAApi", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("WmdaConnectionString")));
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<ISearchMatchService, SearchMatchService>();

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PatientValidator>());
            services.AddTransient<IValidator<Patient>, PatientValidator>();

            ConnectionString = Configuration.GetConnectionString("WmdaConnectionString");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WMDAApi v1"));
            }
            
            app.UseExceptionHandling();

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
