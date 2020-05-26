using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrashCollection.Data;
using TrashCollection.Models;

namespace TrashCollection.Controllers
{
    //[Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            //Customer shouldn't be able to see other Customers
            Customer customer;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                customer= _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();
                
            }
            catch (Exception)
            {
                Customer newcustomer = new Customer();
                ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "day");
                return View("Create",newcustomer);
                
            }

            if (customer.Address.AddressLineOne == null)
            {

                return View("CreateAddressPage",customer.Address);
            }
            var pickups = _context.Pickups.Where(p => p.CustomerId == customer.Id&&p.Confirmed==false);
            ViewBag.pickups = pickups.ToList();
            ViewBag.ConfirmedPickups = _context.Pickups.Where(p => p.CustomerId == customer.Id && p.Confirmed == true);
            foreach (Pickup item in ViewBag.ConfirmedPickups)
            {
                customer.Balance += item.Price;
            }
            return View(customer);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .Include(c => c.IdentityUser)
                .Include(c => c.Weekday)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            Customer customer = new Customer();
            ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "day");
            return View(customer);
        }
        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DayId,FirstName,LastName")] Customer customer)
        {
            if (ModelState.IsValid)
            { 
                //add IdenityUserId to customer object before it gets added to DB
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customer.IdentityUserId = userId;
                Address address = new Address();
                customer.Address = address;

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "Id", customer.DayId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.Where(c => c.Id == id).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();

            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", customer.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "day", customer.DayId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityUserId,AddressId,DayId,FirstName,LastName,SuspendStert,SuspendEnd")] Customer editcustomer)
        {
            if (id != editcustomer.Id)
            {
                return NotFound();
            }
            var customer = _context.Customers.Where(c => c.Id == id).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();
            if (ModelState.IsValid)
            {
                try
                {
                    customer.FirstName = editcustomer.FirstName;
                    customer.LastName = editcustomer.LastName;

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", customer.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "day", customer.DayId);
            return View(customer);
        }
        public async Task<IActionResult> EditDay()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();

            ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "day", customer.DayId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDay( [Bind("DayId")] Customer editcustomer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();


            if (ModelState.IsValid)
            {
                try
                {
                    customer.DayId = editcustomer.DayId;
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "day", customer.DayId);
            return View(customer);
        }
        public async Task<IActionResult> EditName()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditName([Bind("DayId")] Customer editcustomer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();


            if (ModelState.IsValid)
            {
                try
                {
                    customer.DayId = editcustomer.DayId;
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var pickups = _context.Pickups.Where(p => p.CustomerId == customer.Id);
                ViewBag.pickups = pickups.ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "day", customer.DayId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .Include(c => c.IdentityUser)
                .Include(c => c.Weekday)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        public async Task<IActionResult> CreateAddressPage(int? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();

            return View(customer.Address);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAddressPage(Address address)
        {
            //try
            //{
            //    var deleteaddress = _context.Addresses.Where(c => c.Id == address.Id).Single();
            //    _context.Addresses.Remove(deleteaddress);
            //    await _context.SaveChangesAsync();
            //}
            //catch (Exception)
            //{

            //    //if the entry doesnt exist, do nothing.
            //}
            _context.Update(address);
            await _context.SaveChangesAsync();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();
            customer.AddressId = address.Id;
            _context.Update(customer);
            await _context.SaveChangesAsync();
            var pickups = _context.Pickups.Where(p => p.CustomerId == customer.Id);
            ViewBag.pickups = pickups.ToList();
            return View("Index",customer);
        }
        public async Task<IActionResult> EditSuspend()
        {


            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();

           
           
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSuspend([Bind("Id,IdentityUserId,AddressId,DayId,FirstName,LastName,SuspendStart,SuspendEnd")] Customer editcustomer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);    
            
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();
            if (ModelState.IsValid)
            {
                try
                {
                    customer.SuspendStart = editcustomer.SuspendStart;
                    customer.SuspendEnd = editcustomer.SuspendEnd;

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var pickup = _context.Pickups.Where(p => p.CustomerId == customer.Id);
                ViewBag.pickups = pickup.ToList();
                return RedirectToAction(nameof(Index));
            }
            var pickups = _context.Pickups.Where(p => p.CustomerId == customer.Id);
            ViewBag.pickups = pickups.ToList();
            return View(customer);
        }
        public async Task<IActionResult> ClearSuspend()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Include(c => c.Weekday).Single();
            customer.SuspendStart=null;
            customer.SuspendEnd=null;
            _context.Update(customer);
            await _context.SaveChangesAsync();
            var pickups = _context.Pickups.Where(p => p.CustomerId == customer.Id);
            ViewBag.pickups = pickups.ToList();

            return View("Index", customer);
        }

    }
}
