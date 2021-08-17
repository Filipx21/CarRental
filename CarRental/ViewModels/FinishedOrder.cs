using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.ViewModels
{
    public class FinishedOrder
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage="Ta informacja jest wymagana w celu aktualizacji informacji dot. pojazdu")]
        public double StopMileage { get; set; }
        public string UserName { get; set; }
        public decimal TotalCost { get; set; }
        public int TotalDays { get; set; }
    }
}