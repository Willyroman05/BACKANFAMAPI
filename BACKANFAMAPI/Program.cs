using BACKANFAMAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using BACKANFAMAPI.Controllers;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);



//--------------------------------------------------- Configurar Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirOrigenEspecífico",
        builder => builder.WithOrigins("http://10.243.176.194:80", "http://www.expedientedigital_ixchen.com", "http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


//----------------------------------------------- inyector para la base de datos
builder.Services.AddDbContext<AnfamDataBaseContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("coneccion"));
});

//---------------------------------------------fecha
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
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
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// -----------------------------------------------------------Configurar Cors
app.UseCors("PermitirOrigenEspecífico");


app.UseAuthorization();

app.MapControllers();

app.Run();

