namespace CitasMedicas.Models
{
    public class CitasMedico
    {
        public int CitaId { get; set; }

        public int PacienteId { get; set; }

        public int DoctorId { get; set; }

        public DateTime FechaHoraInicio { get; set; }

        public DateTime FechaHoraFin { get; set; }

        public string Estado { get; set; }

        public string Comentarios { get; set; }





        public CitasMedico(int citaId, int pacienteId, int doctorId, DateTime fechaHoraInicio, DateTime fechaHoraFin, string estado, string comentarios)
        {

            this.CitaId = citaId;
            this.PacienteId = pacienteId;
            this.DoctorId = doctorId;
            this.FechaHoraInicio = fechaHoraInicio;
            this.FechaHoraFin = fechaHoraFin;
            this.Estado = estado;
            this.Comentarios = comentarios;


        }

        //Constructor sin DoctorId
        public CitasMedico(int citaId, int pacienteId, DateTime fechaHoraInicio, DateTime fechaHoraFin, string estado, string comentarios)
        {

            this.CitaId = citaId;
            this.PacienteId = pacienteId;
            this.FechaHoraInicio = fechaHoraInicio;
            this.FechaHoraFin = fechaHoraFin;
            this.Estado = estado;
            this.Comentarios = comentarios;
        }


        public CitasMedico WithNewDate(DateTime newFechaInicio, DateTime newFechaFin)
        {

            return new CitasMedico(CitaId, PacienteId, DoctorId, newFechaInicio, newFechaFin, Estado, Comentarios);
        }


        public override string ToString()
        {
            return $"Cita creada con Id: {this.CitaId}, Id de paciente {this.PacienteId}, Doctor Asignado {this.DoctorId}, Incio: {this.FechaHoraInicio}, Fin: {this.FechaHoraFin}," +
                $"Estado: {this.Estado}, Comentarios de la cita: {this.Comentarios}";
        }


        public override bool Equals(object? obj)
        {
            if (obj is CitasMedico citas)
            {
                return this.CitaId == citas.CitaId &&
                       this.PacienteId == citas.PacienteId &&
                       this.DoctorId == citas.DoctorId &&
                       this.FechaHoraInicio == citas.FechaHoraInicio &&
                       this.FechaHoraFin == citas.FechaHoraFin &&
                       this.Estado == citas.Estado &&
                       this.Comentarios == citas.Comentarios;
            }
            return false;
        }


        // Sobrescribir el método GetHashCode
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + CitaId.GetHashCode();
                hash = hash * 23 + PacienteId.GetHashCode();
                hash = hash * 23 + DoctorId.GetHashCode();
                hash = hash * 23 + FechaHoraInicio.GetHashCode();
                hash = hash * 23 + FechaHoraFin.GetHashCode();
                hash = hash * 23 + (Estado != null ? Estado.GetHashCode() : 0);
                hash = hash * 23 + (Comentarios != null ? Comentarios.GetHashCode() : 0);
                return hash;
            }
        }

    }
}
