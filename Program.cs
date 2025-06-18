using ApiExamen.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios para controladores
builder.Services.AddControllers();

// Agregar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Registrar servicios personalizados
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<PuestoService>();
builder.Services.AddScoped<ExamenService>();
builder.Services.AddScoped<ClasificacionService>();
builder.Services.AddScoped<PreguntaService>();
builder.Services.AddScoped<RespuestaService>();
builder.Services.AddScoped<AsignacionService>();
builder.Services.AddScoped<ResultadoService>();

// 🔐 Registrar IConfiguration si usas appsettings.json para la cadena de conexión (opcional pero recomendable)
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// Middleware para habilitar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Proyecto Examen v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz (http://localhost:5000/)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapear controladores
app.MapControllers();

app.Run();
