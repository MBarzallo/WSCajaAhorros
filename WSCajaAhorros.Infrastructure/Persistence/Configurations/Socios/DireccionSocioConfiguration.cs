using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Socios;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Socios;

public class DireccionSocioConfiguration : IEntityTypeConfiguration<DireccionSocio>
{
    public void Configure(EntityTypeBuilder<DireccionSocio> builder)
    {
        builder.ToTable("socios_direcciones");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Etiqueta)
            .HasMaxLength(50);

        builder.Property(d => d.EsPrincipal)
            .IsRequired();

        builder.Property(d => d.EstaActiva)
            .IsRequired();

        // VALUE OBJECT Dirección
        builder.OwnsOne(d => d.Direccion, dir =>
        {
            dir.Property(x => x.Linea1).HasMaxLength(200);
            dir.Property(x => x.Linea2).HasMaxLength(200);
            dir.Property(x => x.Ciudad).HasMaxLength(100);
            dir.Property(x => x.Provincia).HasMaxLength(100);
            dir.Property(x => x.Pais).HasMaxLength(100);
            dir.Property(x => x.Referencia).HasMaxLength(500);
        });
    }
}