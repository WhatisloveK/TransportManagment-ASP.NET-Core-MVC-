using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagment.Models
{
    
    public class Transport
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartOfShipping { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndOfShipping { get; set; }

        [Required]
        public string Departure { get; set; }

        [Required]
        public string Destination { get; set; }

        [Display(Name = "Max weight")]
        [RegularExpression(@"[[0-4]{1}\d{4}|\d{2,4}]$")]
        public int MaxWeight { get; set; }

        [Display(Name = "Load capacity")]
        public int MaxVolume { get; set; }
        [Display(Name ="Truck type")]
        public int TruckTypeID { get; set; }
        public TruckType TruckType { get; set; }

        public string CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
