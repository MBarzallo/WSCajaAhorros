using Microsoft.EntityFrameworkCore;

namespace WSCajaAhorros.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    
}