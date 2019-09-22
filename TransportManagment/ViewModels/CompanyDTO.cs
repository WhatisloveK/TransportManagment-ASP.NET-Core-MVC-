using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TransportManagment.Models
{
    public class CompanyDTO: IdentityUser
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        //public ICollection<Trucker> Truckers { get; set; }
        public ICollection<CargoDTO> Cargoes { get; set; }
        public ICollection<TransportDTO> Transports { get; set; }
    }
}
