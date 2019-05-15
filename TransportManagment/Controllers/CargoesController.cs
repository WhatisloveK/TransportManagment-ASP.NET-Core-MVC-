using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransportManagment.Data;
using TransportManagment.Models;

namespace TransportManagment.Controllers
{
    public class CargoesController : Controller
    {
        private readonly TrnspMngmntContext _context;

        public CargoesController(TrnspMngmntContext context)
        {
            _context = context;
        }

        // GET: Cargoes
        public async Task<IActionResult> Index(/*string sortOrder*/)
        {
            //ViewData["NameSortParm"]=String.IsNullOrEmpty(sortOrder)?
            var trnspMngmntContext = _context.Cargoes.Include(c => c.Company).Include(c => c.TruckType);
            return View(await trnspMngmntContext.ToListAsync());
        }

        // GET: Cargoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargoes
                .Include(c => c.Company)
                .Include(c => c.TruckType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // GET: Cargoes/Create
        public IActionResult Create()
        {
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "Name");
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TruckTypeID");
            return View();
        }

        // POST: Cargoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StartOfShipping,EndOfShipping,Departure,Destination,Info,Weight,Volume,TruckTypeID,CompanyID")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "Name", cargo.CompanyID);
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TruckTypeID", cargo.TruckTypeID);
            return View(cargo);
        }

        // GET: Cargoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargoes.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "Name", cargo.CompanyID);
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TruckTypeID", cargo.TruckTypeID);
            return View(cargo);
        }

        // POST: Cargoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StartOfShipping,EndOfShipping,Departure,Destination,Info,Weight,Volume,TruckTypeID,CompanyID")] Cargo cargo)
        {
            if (id != cargo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExists(cargo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "Name", cargo.CompanyID);
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TruckTypeID", cargo.TruckTypeID);
            return View(cargo);
        }

        // GET: Cargoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargoes
                .Include(c => c.Company)
                .Include(c => c.TruckType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST: Cargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargoes.FindAsync(id);
            _context.Cargoes.Remove(cargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoExists(int id)
        {
            return _context.Cargoes.Any(e => e.ID == id);
        }
    }
}
