using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Socios;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Socios;

public class CorreoSocioConfiguration : IEntityTypeConfiguration<CorreoSocio>
{
    public void Configure(EntityTypeBuilder<CorreoSocio> builder)
    {
        builder.ToTable("socios_correos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.CorreoElectronico)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.Etiqueta)
            .HasMaxLength(50);

        builder.Property(c => c.EsPrincipal)
            .IsRequired();

        builder.Property(c => c.EstaActivo)
            .IsRequired();

        builder.HasIndex(c => c.CorreoElectronico);
    }
}