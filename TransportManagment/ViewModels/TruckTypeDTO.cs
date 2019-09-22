using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagment.Models
{
    public class TruckTypeDTO
    {
        public int TruckTypeID { get; set; }
        public string TypeName { get; set; }
    }
}
