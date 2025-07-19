using API.Middleware;
using Application.Common.Mapping;
using Application.Products.Handlers;
using Application.Products.Interfaces;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssemblies(typeof(GetProductListHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(GetFiltersHandler).Assembly);
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ExceptionMiddleware>();


builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




//Add Cors Policies

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "react-app-client", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

    options.AddPolicy(name: "swagger", builder => 
    { 
        builder.WithOrigins("http://localhost:5097")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();


//Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("react-app-client");
app.UseCors("swagger");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//DbInitializer.InitDb(app);

app.Run();
