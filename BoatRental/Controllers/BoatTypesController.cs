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
    public class BoatTypesController : Controller
    {
        private readonly BoatRentalContext _context;

        public BoatTypesController(BoatRentalContext context)
        {
            _context = context;
        }

        // GET: BoatTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BoatType.ToListAsync());
        }

        // GET: BoatTypes/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: BoatTypes/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,BoatsType")] BoatType boatType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boatType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boatType);
        }

        // GET: BoatTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boatType = await _context.BoatType.FindAsync(id);
            if (boatType == null)
            {
                return NotFound();
            }
            return View(boatType);
        }

        // POST: BoatTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BoatsType")] BoatType boatType)
        {
            if (id != boatType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boatType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoatTypeExists(boatType.Id))
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
            return View(boatType);
        }

        // GET: Countrylist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var cuntrylist = await _context.BoatType.FindAsync(id);
            _context.BoatType.Remove(cuntrylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoatTypeExists(int id)
        {
            return _context.BoatType.Any(e => e.Id == id);
        }
    }
}
