using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Infrastructure.Dapper;
using WSCajaAhorros.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<DbConnectionFactory>(sp =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    return new DbConnectionFactory(cs!);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();