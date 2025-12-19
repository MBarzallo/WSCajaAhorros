using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Security;

public class UsuarioConfiguration: IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.NombreUsuario)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.CorreoElectronico)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(u => u.HashContrasena)
            .IsRequired();

        builder.Property(u => u.SaltContrasena)
            .IsRequired();

        builder.Property(u => u.EstaActivo)
            .IsRequired();

        builder.Property(u => u.MfaHabilitado)
            .IsRequired();

        builder.Property(u => u.FechaCreacion)
            .IsRequired();

        builder.Property(u => u.UltimoInicioSesion)
            .IsRequired(false);

        builder.HasIndex(u => u.NombreUsuario)
            .IsUnique();

        builder.HasIndex(u => u.CorreoElectronico)
            .IsUnique();

        builder.HasMany(u => u.Roles)
            .WithOne(ur => ur.Usuario)
            .HasForeignKey(ur => ur.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.AccesosHorarios)
            .WithOne(ah => ah.Usuario)
            .HasForeignKey(ah => ah.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}