namespace CitasMedicas.Models.Dtos
{
    public class CitasMedicoDto
    {
        public int CitaId { get; set; }

        public int PacienteId { get; set; }

        public int DoctorId { get; set; }

        public DateTime FechaHoraInicio { get; set; }

        public DateTime FechaHoraFin { get; set; }

        public string Estado { get; set; }

        public string Comentarios { get; set; }
    }
}
