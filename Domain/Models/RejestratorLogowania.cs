using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class RejestratorLogowania
    {
        [Key]
        public string RejestratorLogowaniaId { get; set; }
        public string DataZalogowania { get; set; }
        public string DataWylogowania { get; set; }

        public string? UserId { get; set; }
        //public ApplicationUser User { get; set; }
    }
}
