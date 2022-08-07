using Microsoft.EntityFrameworkCore;
using ejercicio1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obtiene la conexion a Base de datos
var stringConnection = builder.Configuration.GetConnectionString("StringConnection");

//EF
builder.Services.AddDbContext<movimientosContext>(options => options.UseSqlServer(stringConnection, e =>
                         e.MigrationsAssembly("Migrators.MSSQL")));

builder.Services.AddControllersWithViews();

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
