using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Movimientos;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Movimientos;

public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
{
    public void Configure(EntityTypeBuilder<Movimiento> builder)
    {
        builder.ToTable("movimientos");

        // PK
        builder.HasKey(m => m.Id);

        // FK Cuenta
        builder.Property(m => m.CuentaId)
            .IsRequired();

        // Tipo movimiento
        builder.Property(m => m.Tipo)
            .HasConversion<int>()
            .IsRequired();

        // Usuario que ejecuta
        builder.Property(m => m.UsuarioId)
            .IsRequired();

        // Canal
        builder.Property(m => m.Canal)
            .HasMaxLength(50)
            .IsRequired();

        // IP
        builder.Property(m => m.DireccionIp)
            .HasMaxLength(45); // IPv4 / IPv6

        // Descripción
        builder.Property(m => m.Descripcion)
            .HasMaxLength(250)
            .IsRequired();

        // Fecha
        builder.Property(m => m.FechaOperacion)
            .IsRequired();

        // Transferencia (nullable)
        builder.Property(m => m.TransferenciaId)
            .IsRequired(false);

        // Asiento contable (nullable)
        builder.Property(m => m.AsientoContableId)
            .IsRequired(false);

        // VALUE OBJECT: Dinero
        builder.OwnsOne(m => m.Monto, dinero =>
        {
            dinero.Property(d => d.Monto)
                .HasColumnName("monto")
                .HasPrecision(18, 2)
                .IsRequired();
        });

        // Índices importantes
        builder.HasIndex(m => m.CuentaId);
        builder.HasIndex(m => m.FechaOperacion);
        builder.HasIndex(m => m.TransferenciaId);
    }
}