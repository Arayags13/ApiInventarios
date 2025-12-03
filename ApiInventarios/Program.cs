using ApiInventarios.BLL.Mapeos;
using ApiInventarios.BLL.Servicios;
using ApiInventarios.DLL;
using ApiInventarios.DLL.RepositorioGenerico;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Configurar la Base de Datos (SQLite)
builder.Services.AddDbContext<Caso2Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 2. Registrar el Repositorio Genérico
// (Es vital para que funcione el servicio, tal como en tu proyecto de referencia)
builder.Services.AddScoped(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));

// 3. Registrar tus Servicios (AQUÍ ESTÁBA EL ERROR)
// Esta línea le dice a la app: "Cuando alguien pida IProductoServicio, dale un ProductoServicio"
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();

// 4. Registrar AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

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