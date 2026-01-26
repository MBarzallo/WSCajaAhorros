using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Socios;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Socios;

public class TelefonoSocioConfiguration : IEntityTypeConfiguration<TelefonoSocio>
{
    public void Configure(EntityTypeBuilder<TelefonoSocio> builder)
    {
        builder.ToTable("socios_telefonos");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Numero)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.Etiqueta)
            .HasMaxLength(50);

        builder.Property(t => t.EsPrincipal)
            .IsRequired();

        builder.Property(t => t.EstaActivo)
            .IsRequired();

        builder.HasIndex(t => new { t.SocioId, t.EsPrincipal });
    }
}