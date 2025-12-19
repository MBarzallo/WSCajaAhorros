using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Security;

public class RolPermisoConfiguration: IEntityTypeConfiguration<RolPermiso>
{
    public void Configure(EntityTypeBuilder<RolPermiso> builder)
    {
        // Tabla
        builder.ToTable("roles_permisos");

        // PK compuesta
        builder.HasKey(rp => new { rp.RolId, rp.PermisoId });

        // Relación con Rol
        builder.HasOne(rp => rp.Rol)
            .WithMany(r => r.Permisos)
            .HasForeignKey(rp => rp.RolId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación con Permiso
        builder.HasOne(rp => rp.Permiso)
            .WithMany(p => p.Roles)
            .HasForeignKey(rp => rp.PermisoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}