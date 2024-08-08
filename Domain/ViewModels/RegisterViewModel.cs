namespace Domain.ViewModels
{
    public class RegisterViewModel
    {
        public string UserId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Ulica { get; set; }
        public string NumerUlicy { get; set; }
        public string Miejscowosc { get; set; }
        public string KodPocztowy { get; set; }
        public string Kraj { get; set; }
        public string DataUrodzenia { get; set; }
        public string Telefon { get; set; }

        public string RoleName { get; set; }

        public string? Token { get; set; }
        public bool? Success { get; set; }
        public string? Message { get; set; }
    }
}
