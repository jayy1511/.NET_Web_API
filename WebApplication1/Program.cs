using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// services + middleware + filters namespaces
using WebApplication1.Services;
using WebApplication1.Middleware;
using WebApplication1.Filters;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers (add a global filter if you want to enforce a header on every request)
            builder.Services.AddControllers(options =>
            {
                // Uncomment to require a custom header on all endpoints:
                // options.Filters.Add(new RequireHeaderAttribute("X-Client"));
            });

            // EF Core → Azure SQL (with resiliency + longer timeout)
            builder.Services.AddDbContext<LibraryDbContext>(opt =>
                opt.UseSqlServer(
                    builder.Configuration.GetConnectionString("LibraryDb"),
                    sql =>
                    {
                        sql.EnableRetryOnFailure(maxRetryCount: 5,
                                                 maxRetryDelay: TimeSpan.FromSeconds(20),
                                                 errorNumbersToAdd: null);
                        sql.CommandTimeout(60);
                    }));

            // =======================
            // Dependency Injection
            // =======================
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            // If you later add an AuthorService/IAuthorService, register it here too:
            // builder.Services.AddScoped<IAuthorService, AuthorService>();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // =======================
            // Middleware pipeline
            // =======================
            // Correlation ID on all requests
            app.UseMiddleware<CorrelationIdMiddleware>();
            // Centralized error handling -> ProblemDetails JSON
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
