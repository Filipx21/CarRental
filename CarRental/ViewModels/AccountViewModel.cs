using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Musisz wprowadzić e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = " Hasło ")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdz Hasło ")]
        [Compare("Password", ErrorMessage = "Hasło i potwierdzenie hasła nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Wprowadz imie użytkownika")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Wprowadz nazwisko użytkownika")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Wprowadz ulice zamieszkania użytkownika")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Wprowadz numer domu/mieszkania użytkownika")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Wprowadz miasto zamieszkania użytkownika")]
        public string City { get; set; }

        [Required(ErrorMessage = "Wprowadz kod pocztowy użytkownika")]
        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Błędny format kodu pocztowego")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Wprowadz numer telefonu użytkownika")]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string Phone { get; set; }
    }
}