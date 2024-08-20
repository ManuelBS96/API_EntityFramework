using API_EntityFramework;
using API_EntityFramework.Filtros;
using API_EntityFramework.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ** Configura servicios ***
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                                                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => 
   options.UseSqlServer(builder.Configuration.GetConnectionString("default"))
   );

builder.Services.AddTransient<IServicio, ServicioA>();
builder.Services.AddTransient<MiFiltroAccion>();
builder.Services.AddHostedService<EscribirEnArchivo>(); 

builder.Services.AddResponseCaching();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
//builder.Services.AddTransient<ServicioA>(); // De esta manera se instancia una clase como servicio
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo {Title="WebApiAutores", Version= "v1" })
);

var app = builder.Build();


// == Confuraciones de la app ==
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
