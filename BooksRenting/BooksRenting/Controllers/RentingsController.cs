using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksRenting.Data;
using BooksRenting.Models;
using System;

namespace BooksRenting.Controllers
{
    public class RentingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rentings
        public async Task<IActionResult> Index()
        {
            var rentings = await _context.Rentings.Include(r => r.Book).ToListAsync();
            return View(rentings);
        }

        // GET: Rentings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var renting = new Renting();
            renting.AvailableBooks = await _context.Books.ToListAsync();
            renting = await _context.Rentings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (renting == null)
            {
                return NotFound();
            }

            return View(renting);
        }

        // GET: Rentings/Create
        public async Task<IActionResult> CreateAsync()
        {
            var renting = new Renting();
            renting.StartDate = DateTime.Today;
            renting.EndDate = DateTime.Today.AddDays(15);
            renting.AvailableBooks = await _context.Books.ToListAsync();
            
            return View(renting);
        }

        // POST: Rentings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,ReturnDate,SelectedBookId")] Renting renting)
        {
            if (ModelState.IsValid)
            {
                var selectedBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == renting.SelectedBookId);
                if (selectedBook is null)
                {
                    ModelState.AddModelError("SelectedBookId", "Cannot find the Book");
                    return View(renting);
                }

                renting.Book = selectedBook;
                _context.Add(renting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(renting);
        }

        // GET: Rentings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renting = await _context.Rentings.FindAsync(id);
            if (renting == null)
            {
                return NotFound();
            }
            renting.StartDate = DateTime.Today;
            renting.EndDate = DateTime.Today.AddDays(15);
            renting = new Renting { AvailableBooks = await _context.Books.ToListAsync() };
            return View(renting);
        }

        // POST: Rentings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,ReturnDate")] Renting renting)
        {
            var selectedBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == renting.SelectedBookId);            
            if (selectedBook is null)
            {
                ModelState.AddModelError("SelectedBookId", "Cannot find the Book");
                return View(renting);
            }

            if (id != renting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    renting.Book = selectedBook;
                    _context.Add(renting);
                    _context.Update(renting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentingExists(renting.Id))
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
            return View(renting);
        }

        // GET: Rentings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var renting = await _context.Rentings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (renting == null)
            {
                return NotFound();
            }

            return View(renting);
        }

        // POST: Rentings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var renting = await _context.Rentings.FindAsync(id);
            _context.Rentings.Remove(renting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentingExists(int id)
        {
            return _context.Rentings.Any(e => e.Id == id);
        }
    }
}
