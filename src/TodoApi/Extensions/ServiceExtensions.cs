using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Infrastructure.Interfaces;
using TodoApi.Infrastructure.Repositories;
using TodoApi.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Add controllers and API explorer for Swagger
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Add AutoMapper for object mapping
        services.AddAutoMapper(typeof(Program));

        // Add FluentValidation - Register validators from the assembly containing Program
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<Program>();

        // Add HTTP context accessor for accessing HttpContext in services
        services.AddHttpContextAccessor();

        // Register AuthService for dependency injection
        services.AddScoped<AuthService>();

        // Register repositories and services
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<CategoryService>();

        services.AddScoped<IPriorityRepository, PriorityRepository>();
        services.AddScoped<PriorityService>();

        services.AddScoped<IToDoListRepository, ToDoListRepository>();
        services.AddScoped<ToDoListService>();

        services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        services.AddScoped<ToDoItemService>();

        // Add DbContext for ApplicationDbContext with SQL Server connection
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}
