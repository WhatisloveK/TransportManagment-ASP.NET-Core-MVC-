using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagment.Models
{
    public class Company:IdentityUser
    {
        
        public string Name { get; set; }
        public string Phone { get; set; }

       //public ICollection<Trucker> Truckers { get; set; }
        public ICollection<Cargo> Cargoes { get; set; }
        public ICollection<Transport> Transports { get; set; }
    }
}
