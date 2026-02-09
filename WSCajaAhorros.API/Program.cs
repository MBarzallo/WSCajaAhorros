using System.Text;
using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.Common.Security;
using WSCajaAhorros.Application.Interfaces.Repositories.Movimientos;
using WSCajaAhorros.Application.Interfaces.Repositories.Socios;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;
using WSCajaAhorros.Application.Interfaces.Services.Movimientos;
using WSCajaAhorros.Application.Interfaces.Services.Security;
using WSCajaAhorros.Application.Interfaces.Services.Socios;
using WSCajaAhorros.Application.Services.Cuentas;
using WSCajaAhorros.Application.Services.Movimientos;
using WSCajaAhorros.Application.Services.Security;
using WSCajaAhorros.Application.Services.Socios;
using WSCajaAhorros.Infrastructure.Dapper;
using WSCajaAhorros.Infrastructure.Persistence;
using WSCajaAhorros.Infrastructure.Repositories;
using WSCajaAhorros.Infrastructure.Repositories.Cuentas;
using WSCajaAhorros.Infrastructure.Repositories.Movimientos;
using WSCajaAhorros.Infrastructure.Repositories.Socios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention();
});
builder.Services.AddScoped<DbConnectionFactory>(sp =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    return new DbConnectionFactory(cs!);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod(); // ← ESTO habilita OPTIONS
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])
            )
        };
    });

builder.Services.AddTransient<ITransferenciaService, TransferenciaService>();
builder.Services.AddTransient<ITransferenciaRepository, TransferenciaRepository>();

builder.Services.AddTransient<IAutenticacionService, AutenticacionService>();
builder.Services.AddTransient<JwtService>();

builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IRolService, RolService>();

builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
builder.Services.AddScoped<IPermisoService, PermisoService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<ICuentaService, CuentaService>();

builder.Services.AddScoped<ISocioRepository, SocioRepository>();
builder.Services.AddScoped<ISociosService, SociosService>();

builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
builder.Services.AddScoped<IMovimientosService, MovimientoService>();

builder.Services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();

builder.Services.AddScoped<IProductoCuentaRepository, ProductoCuentaRepository>();
builder.Services.AddScoped<IProductoCuentaService, ProductoCuentaService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuarioActualService, UsuarioActualService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(app =>
{
    app.Run(async context =>
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsJsonAsync(
            Response.Fail("Error interno del servidor")
        );
    });
});

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();