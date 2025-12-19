using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Infrastructure.Persistence.Configurations.Security;

public class UsuarioAccesoHorarioConfiguration: IEntityTypeConfiguration<UsuarioAccesoHorario>
{
    public void Configure(EntityTypeBuilder<UsuarioAccesoHorario> builder)
    {
        // Tabla
        builder.ToTable("usuarios_accesos_horarios");

        // PK
        builder.HasKey(x => x.Id);

        // FK Usuario
        builder.Property(x => x.UsuarioId)
            .IsRequired();

        builder.HasOne(x => x.Usuario)
            .WithMany(u => u.AccesosHorarios)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Día de la semana (enum → int)
        builder.Property(x => x.DiaSemana)
            .IsRequired()
            .HasConversion<int>();

        // Horarios
        builder.Property(x => x.HoraInicio)
            .IsRequired();

        builder.Property(x => x.HoraFin)
            .IsRequired();

        // Fechas opcionales
        builder.Property(x => x.FechaInicio)
            .IsRequired(false);

        builder.Property(x => x.FechaFin)
            .IsRequired(false);

        // Estado
        builder.Property(x => x.Activo)
            .IsRequired();

        // Índice útil para validaciones rápidas
        builder.HasIndex(x => new 
        { 
            x.UsuarioId, 
            x.DiaSemana, 
            x.HoraInicio, 
            x.HoraFin 
        });
    }
}