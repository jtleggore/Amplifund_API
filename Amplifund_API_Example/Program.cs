
using Amplifund_API_Example.Contexts;
using Amplifund_API_Example.Endpoints;
using Amplifund_API_Example.Repositories;
using Amplifund_API_Example.Validators;

namespace Amplifund_API_Example
{
    public class Program
    {
        public static void AddContextScopes(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepo<Person>, SQLRepo<Person>>();
            builder.Services.AddScoped<IRepo<TestEntity>, SQLRepo<TestEntity>>();
            builder.Services.AddTransient<IValidator<Person>, GenericValidator<Person>>();
            builder.Services.AddTransient<IValidator<TestEntity>, GenericValidator<TestEntity>>();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SqlDataContext>();

            AddContextScopes(builder);

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
