using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Security;

public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
{
    public void Configure(EntityTypeBuilder<Permiso> builder)
    {
        // Tabla
        builder.ToTable("permisos");

        // PK
        builder.HasKey(p => p.Id);

        // Propiedades
        builder.Property(p => p.Codigo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(200);

        // Código único
        builder.HasIndex(p => p.Codigo)
            .IsUnique();

        // Navegación por campo privado (DDD)
        builder.Navigation(p => p.Roles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}