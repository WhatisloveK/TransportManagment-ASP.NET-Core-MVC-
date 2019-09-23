using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using TransportManagment.Models;
using TransportManagment.Services;
using TransportManagment_DAL.Data;
using TransportManagment_DAL.Models;

namespace TransportManagment.Controllers
{
    
    [Authorize(Roles = "user")]
    public class CargoesController : FormatController
    {
        private readonly TrnspMngmntContext _context;
        UserManager<Company> _userManager;
        CargoService _cargoService;

        public CargoesController(TrnspMngmntContext context, UserManager<Company> userManager)
        {
            _context = context;
            _userManager = userManager;
            _cargoService = new CargoService(context, userManager);
        }

        // GET: CargoesList
        public async Task<IActionResult> CargoesList()
        {
            var result = _cargoService.GetCargoDTOs();
            return FormatOrView(result);
        }
        // GET: Cargoes
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); 
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
           // var trnspMngmntContext = _context.Cargoes.Include(c => c.Company).Include(c => c.TruckType).Where(i=>i.CompanyID==user.Id);
            var result = _cargoService.GetCargoDTOs();
            return FormatOrView(result);
        }

        
        // GET: Cargoes/Details/5
        [Route("[controller]/[action]/{id}.{format?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           /* var cargo = await _context.Cargoes
                .Include(c => c.Company)
                .Include(c => c.TruckType)
                .FirstOrDefaultAsync(m => m.ID == id);*/
            var cargo =  _cargoService.GetDetailsAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            return FormatOrView(cargo);
        }

        // GET: Cargoes/Create

        public IActionResult Create()
        {
            ViewData["CompanyID"] = new SelectList(_context.Companies, "Id", "Id");
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TypeName");
            return View();
        }

        // POST: Cargoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{format?}")]
        public async Task<IActionResult> Create([Bind("ID,StartOfShipping,EndOfShipping,Departure,Destination,Info,Weight,Volume,TruckTypeID")] CargoDTO cargo)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                cargo.CompanyID = user.Id;
                _context.Add(cargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "Id", "Id", cargo.CompanyID);
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TruckTypeID", cargo.TruckTypeID);
            return FormatOrView(cargo);
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
            ViewData["CompanyID"] = new SelectList(_context.Companies, "Id", "Id", cargo.CompanyID);
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TypeName", cargo.TruckTypeID);
            return View(cargo);
        }

        // POST: Cargoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StartOfShipping,EndOfShipping,Departure,Destination,Info,Weight,Volume,TruckTypeID,CompanyID")] CargoDTO cargo)
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
            ViewData["CompanyID"] = new SelectList(_context.Companies, "Id", "Id", cargo.CompanyID);
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
