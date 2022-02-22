using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WMDAApi.Models;
using WMDAApi.Services;

namespace WMDAApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    var connectionString = Environment.GetEnvironmentVariable("WmdaConnectionString");
                    services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
                    services.AddScoped<IAppDbContext, AppDbContext>();
                    services.AddScoped<IPatientService, PatientService>();
                    services.AddScoped<ISearchMatchService, SearchMatchService>();
                    services.AddTransient<IValidator<Patient>, PatientValidator>();
                })

            ;
    }
}
