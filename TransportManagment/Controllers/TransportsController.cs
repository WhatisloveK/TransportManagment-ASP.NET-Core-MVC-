using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransportManagment.Data;
using TransportManagment.Models;

namespace TransportManagment.Controllers
{
    [Authorize(Roles = "user")]
    public class TransportsController : Controller
    {
        private readonly TrnspMngmntContext _context;
        UserManager<Company> _userManager;
        public TransportsController(TrnspMngmntContext context, UserManager<Company> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transports
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var trnspMngmntContext = _context.Transports.Include(t => t.Company).Include(t => t.TruckType).Where(i=>i.CompanyID==user.Id);
            return View(await trnspMngmntContext.ToListAsync());
        }

        // GET: Transports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports
                .Include(t => t.Company)
                .Include(t => t.TruckType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // GET: Transports/Create
        public IActionResult Create()
        {
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "Name");
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TypeName");
            return View();
        }

        // POST: Transports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StartOfShipping,EndOfShipping,Departure,Destination,MaxWeight,MaxVolume,Type,TruckTypeID")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                transport.CompanyID = user.Id;
                _context.Add(transport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "Name", transport.CompanyID);
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TruckTypeID", transport.TruckTypeID);
            return View(transport);
        }

        // GET: Transports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "Name", transport.CompanyID);
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TypeName", transport.TruckTypeID);
            return View(transport);
        }

        // POST: Transports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StartOfShipping,EndOfShipping,Departure,Destination,MaxWeight,MaxVolume,Type,TruckTypeID,CompanyID")] Transport transport)
        {
            if (id != transport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportExists(transport.ID))
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
            ViewData["CompanyID"] = new SelectList(_context.Companies, "ID", "Name", transport.CompanyID);
            ViewData["TruckTypeID"] = new SelectList(_context.TruckTypes, "TruckTypeID", "TruckTypeID", transport.TruckTypeID);
            return View(transport);
        }

        // GET: Transports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports
                .Include(t => t.Company)
                .Include(t => t.TruckType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // POST: Transports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            _context.Transports.Remove(transport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportExists(int id)
        {
            return _context.Transports.Any(e => e.ID == id);
        }
    }
}
