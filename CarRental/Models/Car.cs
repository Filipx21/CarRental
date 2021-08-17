using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwe producenta")]
        [StringLength(100, ErrorMessage="Nazwa producenta nie może mieć wiecej niż 100 znaków")]
        public string Producer { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwe modelu pojazdu")]
        [StringLength(100, ErrorMessage = "Modelu pojazdu nie może mieć wiecej niż 100 znaków")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Wprowadz pojemność pojazdu")]
        [Range(0.00, 50.00, ErrorMessage="Pojemność powinna mieścić sie w przedziale 0.00 - 50.0")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double EngineCapacity { get; set; }
        [Required(ErrorMessage = "Wprowadz przebieg pojazdu")]
        [Range(0.00, 1000000.00, ErrorMessage="Przebieg powinna mieścić sie w przedziale 0.00 - 50.0")]
        public double Mileage { get; set; }
        public string Color { get; set; }
        [StringLength(100, ErrorMessage="Nazwa pliku nie może mieć wiecej niż 100 znaków")]
        [FileExtensions(Extensions="png,jpg", ErrorMessage="Plik musi zawierać rozszerzenie png lub jpg")]
        public string PhotoUrl { get; set; }
        public DateTime ProductionDate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsUsed { get; set; }
        [Required(ErrorMessage = "Podaj cenę za kilometr")]
        [Range(0.00, 9999.99, ErrorMessage = "Cena musi mieścić sie w przedziale 0.00 - 9999.99")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal CostPerDay { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual Category Category { get; set; }
    }
}