using Microsoft.EntityFrameworkCore;
using SearchAndMatch.Application.Servives;
using SearchAndMatch.DAL.Context;
using SearchAndMatch.DAL.Repositories;
using SearchAndMatch.Domain.Interfaces;

namespace SearchAndMatch.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add context
            builder.Services
                .AddDbContext<SearchAndMatchContext>(
                    optionBuilder => optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString("wmdaDb"),
                        postgresOptionBuilder => postgresOptionBuilder.MigrationsAssembly("SearchAndMatch.DAL")
                        .MigrationsHistoryTable("__EFMigrationsHistory", "wmda")))
                .AddScoped<IPatientRepository, PatientRepository>()
                .AddScoped<IEngineRepository, EngineRepository>();

            // Add service
            builder.Services.AddHttpClient("Client");
            builder.Services.AddTransient<ICreateSearchEngineService, CreateSearchEngineService>();
            builder.Services.AddTransient<IPatientService, PatientService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}