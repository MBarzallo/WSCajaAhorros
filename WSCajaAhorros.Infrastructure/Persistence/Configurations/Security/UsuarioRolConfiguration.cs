using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Security;

public class UsuarioRolConfiguration: IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(EntityTypeBuilder<UsuarioRol> builder)
    {
        // Tabla
        builder.ToTable("usuarios_roles");

        // PK compuesta
        builder.HasKey(ur => new { ur.UsuarioId, ur.RolId });

        // Relación con Usuario
        builder.HasOne(ur => ur.Usuario)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación con Rol
        builder.HasOne(ur => ur.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(ur => ur.RolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}