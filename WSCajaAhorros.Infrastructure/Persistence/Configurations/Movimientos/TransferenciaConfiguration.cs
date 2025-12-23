using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Movimientos;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Movimientos;

public class TransferenciaConfiguration : IEntityTypeConfiguration<Transferencia>
{
    public void Configure(EntityTypeBuilder<Transferencia> builder)
    {
        builder.ToTable("transferencias");

        // PK
        builder.HasKey(t => t.Id);

        // Cuentas
        builder.Property(t => t.CuentaOrigenId)
            .IsRequired();

        builder.Property(t => t.CuentaDestinoId)
            .IsRequired();

        // Usuario
        builder.Property(t => t.UsuarioId)
            .IsRequired();

        // Canal
        builder.Property(t => t.Canal)
            .HasMaxLength(50)
            .IsRequired();

        // IP
        builder.Property(t => t.DireccionIp)
            .HasMaxLength(45);

        // Observación
        builder.Property(t => t.Observacion)
            .HasMaxLength(250)
            .IsRequired();

        // Código operación
        builder.Property(t => t.CodigoOperacion)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(t => t.CodigoOperacion)
            .IsUnique();

        // Fecha
        builder.Property(t => t.FechaOperacion)
            .IsRequired();

        // VALUE OBJECT: Dinero
        builder.OwnsOne(t => t.Monto, dinero =>
        {
            dinero.Property(d => d.Monto)
                .HasColumnName("monto")
                .HasPrecision(18, 2)
                .IsRequired();
        });

        // Índices
        builder.HasIndex(t => t.CuentaOrigenId);
        builder.HasIndex(t => t.CuentaDestinoId);
        builder.HasIndex(t => t.FechaOperacion);
    }
}