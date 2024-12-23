using BancoW_Back.Contexts;
using BancoW_Back.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agrega autenticación JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!); // Reemplaza con una clave segura
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"]!,
        ValidAudience = jwtSettings["Audience"]!,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//inicializo en contexto
builder.Services.AddSqlServer<BancoWBdContext>(builder.Configuration.GetConnectionString("BancoWBd"));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISimulacionService, SimulacionService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()   // Permitir cualquier origen
                       .AllowAnyHeader()   // Permitir cualquier encabezado
                       .AllowAnyMethod();  // Permitir cualquier método HTTP
            });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Aplicar la política CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
