using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagment.Models
{
    public class TruckerDTO
    {
        public int ID { get; set; }


        public string LastName { get; set; }


        public string FirstMidName { get; set; }

        public DateTime DrivingLicence { get; set; }


        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

    }
}
