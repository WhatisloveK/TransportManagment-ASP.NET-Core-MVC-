using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagment.Models
{
    public class CargoDTO
    {
        public int ID { get; set; }

        public DateTime StartOfShipping { get; set; }

        public DateTime EndOfShipping { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string Info { get; set; }
        public int Weight { get; set; }
        public int Volume { get; set; }


        public int TruckTypeID { get; set; }
        public TruckTypeDTO TruckType { get; set; }

        public string CompanyID { get; set; }
        public CompanyDTO Company { get; set; }
    }
}
