using Microsoft.AspNetCore.Mvc;
using Teffly.DTO;
using Teffly.Models;
using Teffly.Repositories;

namespace Teffly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccine<Vaccine> _repository;

        public VaccineController(IVaccine<Vaccine> repository)
        {
            _repository = repository;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaccineDto>>> GetVaccines()
        {
            var vaccines = await _repository.GetAllAsync();
            var vaccineDtos = vaccines.Select(v => new VaccineDto
            {
                VaccineName = v.VaccineName,
                AgeDuration = v.AgeDuration,
                Benefits = v.Benefits
            }).ToList();

            return Ok(vaccineDtos);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<VaccineDto>> GetVaccine(int id)
        {
            var vaccine = await _repository.GetByIdAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            var vaccineDto = new VaccineDto
            {
                VaccineName = vaccine.VaccineName,
                AgeDuration = vaccine.AgeDuration,
                Benefits = vaccine.Benefits
            };

            return Ok(vaccineDto);
        }




        [HttpPost]
        public async Task<ActionResult<VaccineDto>> CreateVaccine(VaccineDto vaccineDto)
        {
            var vaccine = new Vaccine
            {
                VaccineName = vaccineDto.VaccineName,
                AgeDuration = vaccineDto.AgeDuration,
                Benefits = vaccineDto.Benefits
            };

            await _repository.AddAsync(vaccine);
            vaccineDto.VaccineId = vaccine.VaccineId;

            return CreatedAtAction(nameof(GetVaccine), new { id = vaccineDto.VaccineId }, vaccineDto);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccine(int id, VaccineDto vaccineDto)
        {
            var vaccine = await _repository.GetByIdAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            vaccine.VaccineName = vaccineDto.VaccineName;
            vaccine.AgeDuration = vaccineDto.AgeDuration;
            vaccine.Benefits = vaccineDto.Benefits;

            await _repository.UpdateAsync(vaccine);

            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            var vaccine = await _repository.GetByIdAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}