using System.ComponentModel.DataAnnotations;

namespace Teffly.Models
{
    public class Vaccine
    {
        [Key]
        public int VaccineId { get; set; }

        [Required]
        public string? VaccineName { get; set; }

        [Required]
        public string? AgeDuration { get; set; }

        [Required]
        public string? Benefits { get; set; }
    }
}