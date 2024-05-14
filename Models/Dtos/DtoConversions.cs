namespace CitasMedicas.Models.Dtos
{
    public static class DtoConversions
    {

        public static CitasMedicoDto ToDto(this CitasMedico citas)
        {

            if (citas == null)
            {
                throw new ArgumentNullException(nameof(citas));
            }

            return new CitasMedicoDto
            {

                CitaId = citas.CitaId,
                PacienteId = citas.PacienteId,
                DoctorId = citas.DoctorId,
                FechaHoraInicio = citas.FechaHoraInicio,
                FechaHoraFin = citas.FechaHoraFin,
                Comentarios = citas.Comentarios,
                Estado = citas.Estado

            };

        }


        public static IEnumerable<CitasMedicoDto> ToDtos(this IEnumerable<CitasMedico> citas)
        {
            if (citas == null) throw new ArgumentNullException(nameof(citas));
            return citas.Select(c => c.ToDto());
        }



    }
}
