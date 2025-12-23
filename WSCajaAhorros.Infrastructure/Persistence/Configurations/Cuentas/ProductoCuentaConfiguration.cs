using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Productos;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Cuentas;

public class ProductoCuentaConfiguration : IEntityTypeConfiguration<ProductoCuenta>
{
    public void Configure(EntityTypeBuilder<ProductoCuenta> builder)
    {
        builder.ToTable("productos_cuenta");

        // PK
        builder.HasKey(p => p.Id);

        // Código del producto (AHV, CTE, etc.)
        builder.Property(p => p.Codigo)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(p => p.Codigo)
            .IsUnique();

        // Nombre
        builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .IsRequired();

        // Tipo de producto (enum)
        builder.Property(p => p.Tipo)
            .HasConversion<int>()
            .IsRequired();

        // Tasa de interés
        builder.Property(p => p.TasaInteres)
            .HasPrecision(5, 4) // ej: 0.0525 = 5.25%
            .IsRequired();

        // Reglas operativas
        builder.Property(p => p.PermiteRetiros)
            .IsRequired();

        builder.Property(p => p.PermiteTransferencias)
            .IsRequired();

        builder.Property(p => p.SaldoMinimo)
            .HasPrecision(18, 2)
            .IsRequired();
    }
}