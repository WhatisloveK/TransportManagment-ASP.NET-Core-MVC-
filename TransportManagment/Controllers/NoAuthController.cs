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
        public async Task<IActionResult> CargoesList()
        {
            var trnspMngmntContext = _context.Cargoes.Include(c => c.Company).Include(c => c.TruckType);
            return View(await trnspMngmntContext.ToListAsync());
        }

        // GET: TransportsList
        public async Task<IActionResult> TransportsList()
        {
            var trnspMngmntContext = _context.Transports.Include(t => t.Company).Include(t => t.TruckType);
            return View(await trnspMngmntContext.ToListAsync());
        }
    }
}