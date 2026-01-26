using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Contabilidad;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Contabilidad;

public class AsientoContableConfiguration : IEntityTypeConfiguration<AsientoContable>
{
    public void Configure(EntityTypeBuilder<AsientoContable> builder)
    {
        builder.ToTable("asientos_contables");

        // PK
        builder.HasKey(a => a.Id);

        // Fechas
        builder.Property(a => a.FechaContable)
            .IsRequired();

        builder.Property(a => a.FechaCreacion)
            .IsRequired();

        // Descripción
        builder.Property(a => a.Descripcion)
            .HasMaxLength(300)
            .IsRequired();

        // Usuario que genera el asiento
        builder.Property(a => a.UsuarioId)
            .IsRequired();

        // Referencia a operación (movimiento, transferencia, etc.)
        builder.Property(a => a.ReferenciaOperacionId)
            .IsRequired();

        // Tipo operación contable
        builder.Property(a => a.TipoOperacion)
            .HasConversion<int>()
            .IsRequired();

        // Estado del asiento
        builder.Property(a => a.Estado)
            .HasConversion<int>()
            .IsRequired();

        // VALUE OBJECT: Dinero
        builder.OwnsOne(a => a.Monto, dinero =>
        {
            dinero.Property(d => d.Monto)
                .HasColumnName("monto")
                .HasPrecision(18, 2)
                .IsRequired();
        });

        // Índices útiles
        builder.HasIndex(a => a.FechaContable);
        builder.HasIndex(a => a.Estado);
        builder.HasIndex(a => a.TipoOperacion);
        builder.HasIndex(a => a.ReferenciaOperacionId);
    }
}