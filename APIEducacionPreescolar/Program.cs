using Microsoft.EntityFrameworkCore;
using APIEducacionPreescolar.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar la lectura de las configuraciones desde el archivo appsettings.json
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Configurar EF Core con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar CORS para permitir solicitudes desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://localhost:4200")  // Permitir solicitudes desde el frontend
            .AllowAnyMethod()                     // Permitir cualquier método HTTP (GET, POST, etc.)
            .AllowAnyHeader()                     // Permitir cualquier encabezado
            .AllowCredentials());                 // Permitir el envío de credenciales (si es necesario)
});

// Configuración de JWT Bearer Authentication
var secretKey = builder.Configuration["Jwt:SecretKey"];
Console.WriteLine($"JWT Secret Key: {secretKey}"); // Imprime la clave secreta en la consola

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,  // Puedes validarlo si lo necesitas
            ValidateAudience = false, // Puedes validarlo si lo necesitas
            ValidateLifetime = true, // Verifica que el token no haya expirado
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])) // Cargar la clave secreta desde appsettings.json
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activar CORS
app.UseCors("AllowLocalhost");  // Activar la política CORS

// Configuración de Swagger para desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activar la autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
