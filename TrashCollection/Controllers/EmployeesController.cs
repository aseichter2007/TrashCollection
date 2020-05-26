using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using TrashCollection.Data;
using TrashCollection.Models;

namespace TrashCollection.Controllers
{
   // [Authorize(Roles="Employee")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            Employee employee;
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);  
                employee = _context.Employees.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Single();


            }
            catch (Exception)
            {
                return View("Create");
            }
            
            int day = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            day -= day * 2;
            List<Pickup> todaysScheduledPickups = _context.Pickups.Where(p => p.Date == DateTime.Now.Date&&p.Address.ZipCode==employee.Address.ZipCode).Include(c => c.Address).ToList();
            List<Customer> todaysRegularCustomers = _context.Customers.Where(c => c.DayId == day&&c.Address.ZipCode==employee.Address.ZipCode).Include(c => c.Address).ToList();
            foreach (var pickup in todaysScheduledPickups)
            {
                Customer customer = _context.Customers.Where(c => c.Id == pickup.CustomerId).Single();
                todaysRegularCustomers.Remove(customer);
            }
            foreach (Customer item in todaysRegularCustomers)
            {
                if (item.SuspendStart!=null&&item.SuspendEnd!=null)
                {
                    if (DateTime.Now.Date>=item.SuspendStart&&DateTime.Now.Date<=item.SuspendEnd)
                    {
                        todaysRegularCustomers.Remove(item);
                    }
                }
            }
            
            ViewBag.IrregularPickups = todaysScheduledPickups;
            ViewBag.RegularCustomers = todaysRegularCustomers;
            ViewData["DayId"] = new SelectList(_context.Weekdays, "Id", "day");

            return View(employee); 
        }
        public async Task<IActionResult> Preview(Employee dataemployee)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Employee employee = _context.Employees.Where(c => c.IdentityUserId == userId).Include(c => c.Address).Include(c => c.IdentityUser).Single();

            List<Customer> todaysRegularCustomers = _context.Customers.Where(c => c.DayId == dataemployee.AddressId && c.Address.ZipCode == employee.Address.ZipCode).Include(c => c.Address).ToList();

            return View(todaysRegularCustomers);
        }
        public async Task<IActionResult> Confirm(int? id)
        {
            Pickup pickup = _context.Pickups.Where(p => p.Id == id).Single();
            pickup.Confirmed = true;
            _context.Pickups.Update(pickup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ConfirmRegular(int? id)
        {
            Pickup pickup = new Pickup();
            Customer customer = _context.Customers.Where(p => p.Id == id).Include(c=>c.Address).Single();
            pickup.CustomerId = customer.Id;
            pickup.AddressID = customer.AddressId;
            pickup.Date = DateTime.Now.Date;
            pickup.Confirmed = true;
            pickup.Regular = true;
            if (DateTime.Now.DayOfWeek==DayOfWeek.Saturday)
            {
                pickup.Price = 3.50;
            }
            else if (DateTime.Now.DayOfWeek==DayOfWeek.Sunday)
            {
                pickup.Price = 4.50;
            }
            else
            {
                pickup.Price = 2;
            }
            _context.Pickups.Add(pickup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(e => e.Address)
                .Include(c=>c.Weekday)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            ApiKey key = _context.ApiKeys.Where(k => k.Id == 2).Single();
            string apikey = key.Key;
            ViewBag.key = apikey;
            return View(customer);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Employee employee)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                employee.IdentityUserId = userId;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", employee.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", employee.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityUserId,AddressId,FirstName,LastName")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", employee.AddressId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employee.IdentityUserId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Address)
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
