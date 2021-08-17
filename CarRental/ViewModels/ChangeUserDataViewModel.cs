using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.ViewModels
{
    public class ChangeUserDataViewModel
    {
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