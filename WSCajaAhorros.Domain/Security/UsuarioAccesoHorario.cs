namespace WSCajaAhorros.Domain.Security;

public class UsuarioAccesoHorario
{
    public Guid Id { get; private set; }

    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    public DayOfWeek DiaSemana { get; private set; }
    public TimeSpan HoraInicio { get; private set; }
    public TimeSpan HoraFin { get; private set; }

    public DateOnly? FechaInicio { get; private set; }
    public DateOnly? FechaFin { get; private set; }

    public bool Activo { get; private set; }

    protected UsuarioAccesoHorario() { }

    public UsuarioAccesoHorario(
        Usuario usuario,
        DayOfWeek diaSemana,
        TimeSpan horaInicio,
        TimeSpan horaFin,
        DateOnly? fechaInicio = null,
        DateOnly? fechaFin = null)
    {
        if (horaInicio >= horaFin)
            throw new ArgumentException("Hora inicio debe ser menor a hora fin");

        Id = Guid.NewGuid();
        Usuario = usuario;
        UsuarioId = usuario.Id;

        DiaSemana = diaSemana;
        HoraInicio = horaInicio;
        HoraFin = horaFin;

        FechaInicio = fechaInicio;
        FechaFin = fechaFin;

        Activo = true;
    }

    public void Desactivar() => Activo = false;
}
