using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Ulica { get; set; }
        public string NumerUlicy { get; set; }
        public string Miejscowosc { get; set; }
        public string KodPocztowy { get; set; }
        public string Kraj { get; set; }
        public string DataUrodzenia { get; set; }
        public string Telefon { get; set; }
        public string? DataDodania { get; set; }

        public string? RoleId { get; set; }
        //public ApplicationRole Role { get; set; }

        //public List<Product>? Products { get; set; }

        //public List <RejestratorLogowania> RejestratorLogowania { get; set; } 
        //public List <LogException> LogExceptions { get; set; }
    }
}
