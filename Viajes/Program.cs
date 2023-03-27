using Microsoft.EntityFrameworkCore;
using Viajes.Entities;
using Viajes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("ConexionDb");
builder.Services.AddDbContext<ViajesContext>(o => o.UseSqlServer(connectionString));
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<ICiudadService, CiudadService>();
builder.Services.AddScoped<IViajeService, ViajeService>();
builder.Services.AddScoped<ITipoVehiculoService, TipoVehiculoService>();


builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o =>
{
    o.WithOrigins("*");
    o.AllowAnyMethod();
    o.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
