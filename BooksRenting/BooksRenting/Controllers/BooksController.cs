﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksRenting.Data;
using BooksRenting.Models;
using System;

namespace BooksRenting.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public object Title { get; private set; }

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var listOfBooks= await _context.Books.Include(b => b.Author).Include(b => b.Category).ToListAsync();
            return View(listOfBooks);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = new Book();
            book.AvailableAuthors = await _context.Authors.ToListAsync();
            book.AvailableCategories = await _context.Categories.ToListAsync();
            book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            var selectedAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.SelectedAuthorId);
            var selectedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == book.SelectedCategoryId);
            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> CreateAsync()
        {
            var book = new Book();
            book.AvailableAuthors = await _context.Authors.ToListAsync();
            book.AvailableCategories= await _context.Categories.ToListAsync();
            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,SelectedAuthorId,SelectedCategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {               
               
                var selectedAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.SelectedAuthorId);
                if(selectedAuthor is null)
                {
                    ModelState.AddModelError("SelectedAuthorId", "Cannot find the Author");
                    return View(book);
                }

                var selectedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == book.SelectedCategoryId);
                if (selectedCategory is null)
                {
                    ModelState.AddModelError("SelectedCategoryId", "Cannot find the Category");
                    return View(book);
                }

                book.Author = selectedAuthor;
                book.Category = selectedCategory;
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> EditAsync(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            book = new Book
            {
                AvailableAuthors = await _context.Authors.ToListAsync(),
                AvailableCategories = await _context.Categories.ToListAsync()
            };
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,SelectedAuthorId,SelectedCategoryId")] Book book)
        {
            var selectedAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.SelectedAuthorId);
            if (selectedAuthor is null)
            {
                ModelState.AddModelError("SelectedAuthorId", "Cannot find the Author");
                return View(book);
            }

            var selectedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == book.SelectedCategoryId);
            if (selectedCategory is null)
            {
                ModelState.AddModelError("SelectedCategoryId", "Cannot find the Category");
                return View(book);
            }

            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    book.Author = selectedAuthor;
                    book.Category = selectedCategory;
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        private bool BookExists(Category category)
        {
            throw new NotImplementedException();
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);

            var bookHasRentings = await _context.Rentings.Include(r => r.Book).AnyAsync(r => r.Book.Id == id);
            if (bookHasRentings)
            {
                ModelState.AddModelError("", "Book is already rented and cannot be deleted. Delete the rentings first, then try again.");
                return View(book);
            }
            
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
