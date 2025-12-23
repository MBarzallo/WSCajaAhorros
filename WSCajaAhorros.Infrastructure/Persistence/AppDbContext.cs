using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Domain.Security;
using WSCajaAhorros.Domain.Socios;
using WSCajaAhorros.Domain.Cuentas;
using WSCajaAhorros.Domain.Movimientos;
using WSCajaAhorros.Domain.Productos;

namespace WSCajaAhorros.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Permiso> Permisos => Set<Permiso>();
    public DbSet<RolPermiso> RolPermisos => Set<RolPermiso>();
    public DbSet<UsuarioAccesoHorario> UsuarioAccesoHorarios => Set<UsuarioAccesoHorario>();
    public DbSet<UsuarioRol> UsuarioRoles => Set<UsuarioRol>();
    
    
    public DbSet<Socio> Socios => Set<Socio>();
    public DbSet<TelefonoSocio> TelefonosSocios => Set<TelefonoSocio>();
    public DbSet<CorreoSocio> CorreosSocios => Set<CorreoSocio>();
    public DbSet<DireccionSocio> DireccionesSocios => Set<DireccionSocio>();
    
    
    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<ProductoCuenta> ProductoCuentas => Set<ProductoCuenta>();
    
    public DbSet<Movimiento> Movimientos => Set<Movimiento>();
    public DbSet<Transferencia> Transferencias => Set<Transferencia>();

    
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}