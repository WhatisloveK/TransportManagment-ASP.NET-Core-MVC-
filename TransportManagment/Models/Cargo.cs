using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportManagment.Models
{
    public class Cargo
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartOfShipping { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndOfShipping { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string Info { get; set; }
        public int Weight { get; set; }
        public int Volume { get; set; }

        [Display(Name = "Truck type")]
        public int TruckTypeID { get; set; } 
        public TruckType TruckType { get; set; }

        public string CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
