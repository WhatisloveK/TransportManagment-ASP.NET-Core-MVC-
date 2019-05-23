using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportManagment.Data;

namespace TransportManagment.Controllers
{
    public class NoAuthController : Controller
    {
        private readonly TrnspMngmntContext _context;
       

        public NoAuthController(TrnspMngmntContext context)
        {
            _context = context;
        }

        // GET: CargoesList
        public async Task<IActionResult> CargoesList(string sortOrder)
        {
            ViewData["StartSortParm"] = String.IsNullOrEmpty(sortOrder) ? "start_desc" : "";
            ViewData["EndSortParm"] = sortOrder == "End" ? "end_desc" : "End";
            ViewData["DepartureSortParm"] = sortOrder == "Depart" ? "depart_desc" : "Depart";
            ViewData["DestinationSortParm"] = sortOrder == "Dest" ? "dest_desc" : "Dest";
            ViewData["InfoSortParm"] = sortOrder == "Info" ? "info_desc" : "Info";
            ViewData["WeightSortParm"] = sortOrder == "Weight" ? "weight_desc" : "Weight";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "volume_desc" : "Volume";
            ViewData["TruckTypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["CompanySortParm"] = sortOrder == "Company" ? "conpany_desc" : "Company";
            var cargos = from s in _context.Cargoes.Include(c => c.Company).Include(c => c.TruckType) select s;
            switch (sortOrder)
            {
                case "start_desc":
                   cargos = cargos.OrderByDescending(s => s.StartOfShipping);
                    break;
                case "Depart":
                    cargos = cargos.OrderBy(s => s.Departure);
                    break;
                case "depart_desc":
                    cargos = cargos.OrderByDescending(s => s.Departure);
                    break;
                case "Dest":
                    cargos = cargos.OrderBy(s => s.Destination);
                    break;
                case "dest_desc":
                    cargos = cargos.OrderByDescending(s => s.Destination);
                    break;
                case "Info":
                    cargos = cargos.OrderBy(s => s.Info);
                    break;
                case "info_desc":
                    cargos = cargos.OrderByDescending(s => s.Info);
                    break;
                case "Weight":
                    cargos = cargos.OrderBy(s => s.Weight);
                    break;
                case "weight_desc":
                    cargos = cargos.OrderByDescending(s => s.Weight);
                    break;
                case "Volume":
                    cargos = cargos.OrderBy(s => s.Volume);
                    break;
                case "volume_desc":
                    cargos = cargos.OrderByDescending(s => s.Volume);
                    break;
                case "Type":
                    cargos = cargos.OrderBy(s => s.TruckType);
                    break;
                case "type_desc":
                    cargos = cargos.OrderByDescending(s => s.TruckType);
                    break;
                case "Company":
                    cargos = cargos.OrderBy(s => s.CompanyID);
                    break;
                case "company_desc":
                    cargos = cargos.OrderByDescending(s => s.CompanyID);
                    break;
                default:
                    cargos = cargos.OrderBy(s => s.StartOfShipping);
                    break;
            }
            
            return View(await cargos.AsNoTracking().ToListAsync());
        }

        // GET: TransportsList
        public async Task<IActionResult> TransportsList(string sortOrder,string searchDestination,string searchDeparture,string searchType,
            string searchCompany,DateTime searchStartShipping,DateTime searchEndShipping,int searchWeight,int searchVolume)
        {
            ViewData["StartSortParm"] = String.IsNullOrEmpty(sortOrder) ? "start_desc" : "";
            ViewData["EndSortParm"] = sortOrder == "End" ? "end_desc" : "End";
            ViewData["DepartureSortParm"] = sortOrder == "Depart" ? "depart_desc" : "Depart";
            ViewData["DestinationSortParm"] = sortOrder == "Dest" ? "dest_desc" : "Dest";
            ViewData["WeightSortParm"] = sortOrder == "Weight" ? "weight_desc" : "Weight";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "volume_desc" : "Volume";
            ViewData["TruckTypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["CompanySortParm"] = sortOrder == "Company" ? "conpany_desc" : "Company";
            ViewData["DestinationFilter"] = searchDestination;
            ViewData["DepartureFilter"] = searchDeparture;
            ViewData["WeightFilter"] = searchWeight;
            ViewData["VolumeFilter"] = searchVolume;
            ViewData["CompanyFilter"] = searchCompany;
            ViewData["TypeFilter"] = searchType;
            ViewData["StartShippingFilter"] = searchStartShipping;
            ViewData["EndShippingFilter"] = searchEndShipping;

            var transp = from s in _context.Transports.Include(t => t.Company).Include(t => t.TruckType) select s;

            if (!String.IsNullOrEmpty(searchDestination))
            {
                transp = transp.Where(s => s.Destination.Contains(searchDestination));
            }
            if (!String.IsNullOrEmpty(searchDeparture))
            {
                transp = transp.Where(s => s.Departure.Contains(searchDeparture));
            }
            if (searchWeight>0)
            {
                transp = transp.Where(s => s.MaxWeight >= searchWeight);
            }
            if (searchVolume > 0)
            {
                transp = transp.Where(s => s.MaxVolume >= searchVolume);
            }
            if (!String.IsNullOrEmpty(searchType))
            {
                transp = transp.Where(s => s.TruckType.TypeName.Contains(searchType));
            }
            if (!String.IsNullOrEmpty(searchCompany))
            {
                transp = transp.Where(s => s.Company.UserName.Contains(searchCompany));
            }
            if (searchStartShipping != DateTime.Parse("1/1/0001"))
            {
                transp = transp.Where(s => s.StartOfShipping == searchStartShipping);
            }
            if (searchEndShipping != DateTime.Parse("1/1/0001"))
            {
                transp = transp.Where(s => s.EndOfShipping == searchEndShipping);
            }
            switch (sortOrder)
            {
                case "start_desc":
                    transp = transp.OrderByDescending(s => s.StartOfShipping);
                    break;
                case "Depart":
                    transp = transp.OrderBy(s => s.Departure);
                    break;
                case "depart_desc":
                    transp = transp.OrderByDescending(s => s.Departure);
                    break;
                case "Dest":
                    transp = transp.OrderBy(s => s.Destination);
                    break;
                case "dest_desc":
                    transp = transp.OrderByDescending(s => s.Destination);
                    break;
                case "Weight":
                    transp = transp.OrderBy(s => s.MaxWeight);
                    break;
                case "weight_desc":
                    transp = transp.OrderByDescending(s => s.MaxWeight);
                    break;
                case "Volume":
                    transp = transp.OrderBy(s => s.MaxVolume);
                    break;
                case "volume_desc":
                    transp = transp.OrderByDescending(s => s.MaxVolume);
                    break;
                case "Type":
                    transp = transp.OrderBy(s => s.TruckType);
                    break;
                case "type_desc":
                    transp = transp.OrderByDescending(s => s.TruckType);
                    break;
                case "Company":
                    transp = transp.OrderBy(s => s.CompanyID);
                    break;
                case "company_desc":
                    transp = transp.OrderByDescending(s => s.CompanyID);
                    break;
                default:
                    transp = transp.OrderBy(s => s.StartOfShipping);
                    break;
            }

            return View(await transp.AsNoTracking().ToListAsync());
        }
    }
}