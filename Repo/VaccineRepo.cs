using Teffly.Models;

namespace Teffly.Repositories
{
    public class VaccineRepo : IVaccine<Vaccine>
    {
        private static List<Vaccine> vaccines = new List<Vaccine>();

        public Task<IEnumerable<Vaccine>> GetAllAsync()
        {
            return Task.FromResult(vaccines.AsEnumerable());
        }

        public Task<Vaccine> GetByIdAsync(int id)
        {
            var vaccine = vaccines.FirstOrDefault(v => v.VaccineId == id);
            return Task.FromResult(vaccine);
        }

        public Task AddAsync(Vaccine entity)
        {
            entity.VaccineId = vaccines.Count + 1;
            vaccines.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Vaccine entity)
        {
            var vaccine = vaccines.FirstOrDefault(v => v.VaccineId == entity.VaccineId);
            if (vaccine != null)
            {
                vaccine.VaccineName = entity.VaccineName;
                vaccine.AgeDuration = entity.AgeDuration;
                vaccine.Benefits = entity.Benefits;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var vaccine = vaccines.FirstOrDefault(v => v.VaccineId == id);
            if (vaccine != null)
            {
                vaccines.Remove(vaccine);
            }
            return Task.CompletedTask;
        }
    }
}