using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public string UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double StartMileage { get; set; }
        public double StopMileage { get; set; }
        public decimal TotalCost { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwe klienta")]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Wprowadz adres klienta")]
        [StringLength(50)]
        public string CustomerAddress { get; set; }
        [Required(ErrorMessage = "Wprowadz numer telefonu klienta")]
        [StringLength(15)]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string CustomerPhone { get; set; }
        [Required(ErrorMessage = "Wprowadz adres e-mail klienta")]
        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        [StringLength(50)]
        public string CustomerEmail { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Car Car { get; set; }
    }
}