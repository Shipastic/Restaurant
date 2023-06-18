using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ProductAPI.DbContext;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Restaurant.Services.ProductAPI;
using Restaurant.Services.ProductAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
string connetcion = builder.Configuration.GetConnectionString("DefaultConnection");

//Add AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Add Service ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connetcion));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reuastarant.Services.ProductAPI", Version = "v1" });
});

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
