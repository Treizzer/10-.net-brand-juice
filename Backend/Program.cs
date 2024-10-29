
using Backend.Automappers;
using Backend.DTOs;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Backend {
    public class Program {
        public static void Main (string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson();
            // builder.Services.AddSingleton<IPeopleService, PeopleService>();
            // Testing KeyedSingleton
            builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
            builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

            // Testing Keyed Singleton, Scoped and Transient
            builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
            builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
            builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

            builder.Services.AddScoped<IPostService, PostService>();

            builder.Services.AddKeyedScoped<ICommonService<JuiceDto, JuiceInsertDto, JuiceUpdateDto>, JuiceService>("juiceService");
            builder.Services.AddKeyedScoped<ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto>, BrandService>("brandService");

            // -------------------------------------------------------
            // HttpClient Service jsonplaceholder
            builder.Services.AddHttpClient<IPostService, PostService>(c => {
                c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
            });

            // Repository
            builder.Services.AddScoped<IRepository<Juice>, JuiceRepository>();
            builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();

            // Entity Framework configuration
            builder.Services.AddDbContext<StoreContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
            });

            // Validators
            builder.Services.AddScoped<IValidator<JuiceInsertDto>, JuiceInsertValidator>();
            builder.Services.AddScoped<IValidator<JuiceUpdateDto>, JuiceUpdateValidator>();

            // Mappers
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
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
