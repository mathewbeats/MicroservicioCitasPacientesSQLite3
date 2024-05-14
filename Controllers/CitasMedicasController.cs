using CitasMedicas.Models;
using CitasMedicas.Models.Dtos;
using CitasMedicas.Repository;
using CitasMedicas.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicas.Controllers
{
    [Route("api/citas")]
    [ApiController]
    public class CitasMedicasController : ControllerBase
    {

        private readonly ISqlRepository _SqlRepository;


        public CitasMedicasController(ISqlRepository sqlRepository)
        {

            _SqlRepository = sqlRepository;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitasMedico>>> GetAllCitas()
        {

            var citasMedicas = await _SqlRepository.GetAllCitas();

            if (!citasMedicas.Any())
            {
                return NotFound();
            }

            var citasDto = citasMedicas.ToDtos();


            return Ok(citasDto);
        }


        [HttpGet("{id:int}", Name = "CitasId")]
        public async Task<ActionResult<CitasMedico>>GetCitasId(int id)
        {

            var citas = await _SqlRepository.GetCitaMedicaById(id);

            if(citas == null  || citas.CitaId == 0)
            {
                return NotFound();
            }

            var citasIdDto = citas.ToDto();

            return Ok(citasIdDto);

        }


       



    }
}
