using BancoW_Back.Contexts;
using BancoW_Back.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//inicializo en contexto
builder.Services.AddSqlServer<BancoWBdContext>(builder.Configuration.GetConnectionString("BancoWBd"));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISimulacionService, SimulacionService>();

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

app.UseAuthorization();

// Aplicar la política CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
