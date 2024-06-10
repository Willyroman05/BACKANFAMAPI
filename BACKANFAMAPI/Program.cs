using BACKANFAMAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//--------------------------------------------------- Configurar Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirOrigenEspecífico",
        builder => builder.WithOrigins("http://localhost:3002")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


//----------------------------------------------- inyector para la base de datos
builder.Services.AddDbContext<AnfamDataBaseContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("coneccion"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// -----------------------------------------------------------Configurar Cors
app.UseCors("PermitirOrigenEspecífico");


app.UseAuthorization();

app.MapControllers();

app.Run();
