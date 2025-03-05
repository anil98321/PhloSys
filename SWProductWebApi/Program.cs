
using SWProductWebApi.Repositores;
using SWProductWebApi.Services;

namespace SWProductWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            //Enable Cors
            builder.Services.AddCors(options => {
                options.AddPolicy("SWCorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
             }); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
