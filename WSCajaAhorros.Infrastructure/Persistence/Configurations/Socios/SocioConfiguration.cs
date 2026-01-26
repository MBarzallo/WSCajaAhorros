namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Socios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Socios;

public class SocioConfiguration : IEntityTypeConfiguration<Socio>
{
    public void Configure(EntityTypeBuilder<Socio> builder)
    {
        builder.ToTable("socios");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.TipoPersona)
            .IsRequired();

        builder.Property(s => s.EstaActivo)
            .IsRequired();

        builder.Property(s => s.FechaIngreso)
            .IsRequired();

        builder.Property(s => s.FechaActualizacion)
            .IsRequired(false);

        // PERSONA NATURAL
        builder.Property(s => s.Nombres)
            .HasMaxLength(150);

        builder.Property(s => s.Apellidos)
            .HasMaxLength(150);

        // PERSONA JURÍDICA
        builder.Property(s => s.RazonSocial)
            .HasMaxLength(200);

        builder.Property(s => s.NombreComercial)
            .HasMaxLength(200);

        // OWNED VALUE OBJECT
        builder.OwnsOne(s => s.Identificacion, id =>
        {
            id.Property(i => i.Tipo)
                .HasColumnName("tipo_identificacion")
                .IsRequired();

            id.Property(i => i.Numero)
                .HasColumnName("numero_identificacion")
                .HasMaxLength(20)
                .IsRequired();

            id.HasIndex(i => i.Numero).IsUnique();
        });

        // RELACIONES
        builder.HasMany(s => s.Telefonos)
            .WithOne()
            .HasForeignKey(t => t.SocioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Correos)
            .WithOne()
            .HasForeignKey(c => c.SocioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Direcciones)
            .WithOne()
            .HasForeignKey(d => d.SocioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
