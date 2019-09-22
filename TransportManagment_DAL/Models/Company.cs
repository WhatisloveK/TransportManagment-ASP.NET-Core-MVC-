using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace TransportManagment_DAL.Models
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
