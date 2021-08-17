using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage="Wprowadz nazwe typu pojazdu")]
        [StringLength(60, ErrorMessage="Nazwa typu nie może mieć wiecej niż 60")]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        [Required(ErrorMessage = "Wprowadz krótka nazwe pojazdu")]
        [StringLength(10, ErrorMessage = "Krótka nazwa typu nie może mieć wiecej niż 10")]
        public string ShortName { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}