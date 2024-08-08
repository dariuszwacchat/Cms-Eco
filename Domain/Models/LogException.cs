using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class LogException
    {
        [Key]
        public string LogExceptionId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }

        public string? UserId { get; set; }
        //public ApplicationUser? User { get; set; }
    }
}
