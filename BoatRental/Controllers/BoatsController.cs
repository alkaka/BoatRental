using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoatRental.Data;
using BoatRental.Models;

namespace BoatRental.Controllers
{
    public class BoatsController : Controller
    {
        private readonly BoatRentalContext _context;

        public BoatsController(BoatRentalContext context)
        {
            _context = context;
        }

        // GET: Boats
        public async Task<IActionResult> Index()
        {
            var boatRentalContext = _context.Boat.Include(b => b.BoatType);
            return View(await boatRentalContext.ToListAsync());
        }

        // GET: Boats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boat
                .Include(b => b.BoatType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boat == null)
            {
                return NotFound();
            }

            return View(boat);
        }

        // GET: Boats/Add
        public IActionResult Add()
        {
            ViewData["BoatTypeId"] = new SelectList(_context.BoatType, "Id", "BoatsType");
            return View();
        }

        // POST: Boats/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,BoatNmber,BookingNmber,CheckInTime,CustomerPersonalNumber,BoatTypeId")] Boat boat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoatTypeId"] = new SelectList(_context.BoatType, "Id", "BoatsType", boat.BoatTypeId);
            return View(boat);
        }

        // GET: Boats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boat.FindAsync(id);
            if (boat == null)
            {
                return NotFound();
            }
            ViewData["BoatTypeId"] = new SelectList(_context.BoatType, "Id", "BoatsType", boat.BoatTypeId);
            return View(boat);
        }

        // POST: Boats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BoatNmber,BookingNmber,CheckInTime,CustomerPersonalNumber,BoatTypeId")] Boat boat)
        {
            if (id != boat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoatExists(boat.Id))
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
            ViewData["BoatTypeId"] = new SelectList(_context.BoatType, "Id", "BoatsType", boat.BoatTypeId);
            return View(boat);
        }


        // GET: Boats/CheckOut/5
        public async Task<IActionResult> CheckOut(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Boat = await _context.Boat.FirstOrDefaultAsync(m => m.Id == id);

            var checkouttime = DateTime.Now;
            ViewBag.checkouttime = checkouttime;

            var totalTime = checkouttime - Boat.CheckInTime;
            string totalTime2 = string.Format("{0:D2}:{1:D2}:{2:D2}", totalTime.Days, totalTime.Hours, totalTime.Minutes);
            ViewBag.TotalTime = totalTime2;

            string Price = String.Format("{0:F2}", totalTime.TotalHours * 20);
            ViewBag.Price = Price;

            if (Boat == null)
            {
                return NotFound();
            }
            return View(Boat);
        }

        // POST: Boats/CheckOut/5
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutConfirmed(int id)
        {
            var boat = await _context.Boat.FindAsync(id);
            var model = new RecieptViewModel
            {
                BookingNmber = boat.BookingNmber,
                CustomerPersonalNumber = boat.CustomerPersonalNumber,
                CheckInTime = boat.CheckInTime,
                CheckOutTime = DateTime.Now,
                BoatType = _context.BoatType.Any(v => v.Id == boat.BoatTypeId) ?
                                    _context.BoatType.First(v => v.Id == boat.BoatTypeId).BoatsType : ""
            };

            var Parkedhours = model.CheckOutTime - model.CheckInTime;
            model.TotalTime = string.Format("{0:D2}:{1:D2}:{2:D2}", Parkedhours.Days, Parkedhours.Hours, Parkedhours.Minutes);

            if (boat.BoatTypeId == 1) // small boat 
            {
                model.Price = String.Format("{0:F2}", Parkedhours.TotalHours * 20 + 50); ;
            }
            else if (boat.BoatTypeId == 3) // Medium Boat
            {
                model.Price = String.Format("{0:F2}", Parkedhours.TotalHours * (20 * 1.3) + (50 * 1.2)); ;
            }
            else // Big Boat number 2
            {
                model.Price = String.Format("{0:F2}", Parkedhours.TotalHours * (20 * 1.5) + (50 * 1.5)); ;
            }

            //model.Price = String.Format("{0:F2}", Parkedhours.TotalHours * 20); ;
            _context.Boat.Remove(boat);
            await _context.SaveChangesAsync();
            return View("Receipt", model);
        }


        private bool BoatExists(int id)
        {
            return _context.Boat.Any(e => e.Id == id);
        }
    }
}