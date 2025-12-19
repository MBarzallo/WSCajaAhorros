using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Security;

public class RolConfiguration: IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        // Tabla
        builder.ToTable("roles");

        // PK
        builder.HasKey(r => r.Id);

        // Propiedades
        builder.Property(r => r.Codigo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Descripcion)
            .IsRequired()
            .HasMaxLength(150);

        // Índice único por código
        builder.HasIndex(r => r.Codigo)
            .IsUnique();

        // Relaciones se configuran desde las tablas intermedias
        builder.Navigation(r => r.Usuarios).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(r => r.Permisos).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}