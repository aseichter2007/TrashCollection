using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrashCollection.Data;
using TrashCollection.Models;

namespace TrashCollection.Controllers
{
    public class PickupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PickupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pickups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pickups.Include(p => p.Address).Include(p => p.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pickups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickup = await _context.Pickups
                .Include(p => p.Address)
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pickup == null)
            {
                return NotFound();
            }

            return View(pickup);
        }

        // GET: Pickups/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: Pickups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,AddressId,Date,regular,Confirmed,earlyPickup,price")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pickup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", pickup.AddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", pickup.CustomerId);
            return View(pickup);
        }

        // GET: Pickups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickup = await _context.Pickups.FindAsync(id);
            if (pickup == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", pickup.AddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", pickup.CustomerId);
            return View(pickup);
        }

        // POST: Pickups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,AddressId,Date,regular,Confirmed,earlyPickup,price")] Pickup pickup)
        {
            if (id != pickup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pickup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PickupExists(pickup.Id))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", pickup.AddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", pickup.CustomerId);
            return View(pickup);
        }

        // GET: Pickups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickup = await _context.Pickups
                .Include(p => p.Address)
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pickup == null)
            {
                return NotFound();
            }

            return View(pickup);
        }

        // POST: Pickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pickup = await _context.Pickups.FindAsync(id);
            _context.Pickups.Remove(pickup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PickupExists(int id)
        {
            return _context.Pickups.Any(e => e.Id == id);
        }
    }
}
