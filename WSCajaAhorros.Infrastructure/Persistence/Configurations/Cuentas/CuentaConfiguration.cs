using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Cuentas;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Cuentas;

public class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
{
    public void Configure(EntityTypeBuilder<Cuenta> builder)
    {
        builder.ToTable("cuentas");

        // PK
        builder.HasKey(c => c.Id);

        // Número de cuenta
        builder.Property(c => c.NumeroCuenta)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(c => c.NumeroCuenta)
            .IsUnique();

        // Relaciones (FKs)
        builder.Property(c => c.SocioId)
            .IsRequired();

        builder.Property(c => c.ProductoCuentaId)
            .IsRequired();

        // Estado de la cuenta (enum)
        builder.Property(c => c.Estado)
            .HasConversion<int>()   // guarda el enum como int
            .IsRequired();

        // Fecha apertura
        builder.Property(c => c.FechaApertura)
            .IsRequired();

        // VALUE OBJECT: Dinero
        builder.OwnsOne(c => c.Saldo, saldo =>
        {
            saldo.Property(s => s.Monto)
                .HasColumnName("saldo")
                .HasPrecision(18, 2)
                .IsRequired();
        });
    }
}