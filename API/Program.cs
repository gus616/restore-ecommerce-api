using API.Middleware;
using Application.Account.Handlers;
using Application.Common.Mapping;
using Application.Products.Handlers;
using Application.Products.Interfaces;
using Identity.Interfaces;
using Identity.Services;
using Infrastructure.Identity.Entities;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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
    cfg.RegisterServicesFromAssemblies(typeof(RegisterCommandHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(LoginCommandHandler).Assembly);
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IRegisterService, RegisterService>();
builder.Services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddTransient<ExceptionMiddleware>();


builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<StoreDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add authentication (JWT)

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
         };
     }
     );




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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "azure-app-client", builder =>
    {
        builder.WithOrigins("https://nice-ocean-0db720610.1.azurestaticapps.net")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
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
app.UseCors("azure-app-client");
app.UseCors("swagger");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//DbInitializer.InitDb(app);

app.Run();
