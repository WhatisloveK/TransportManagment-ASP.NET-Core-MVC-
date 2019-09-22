using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagment_DAL.Models
{
    public class TruckType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TruckTypeID { get; set; }
        public string TypeName { get; set; }
        
        //public ICollection<Cargo> Cargos { get; set; }
    }
}
