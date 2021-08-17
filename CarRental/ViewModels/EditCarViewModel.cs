using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarRental.Models;

namespace CarRental.ViewModels
{
    public class EditCarViewModel
    {
        public Car Car { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool Confirm { get; set; }
    }
}