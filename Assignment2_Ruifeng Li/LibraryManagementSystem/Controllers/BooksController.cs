using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

public class BooksController(IUnitOfWork unit, LibraryDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Books.Include(b => b.Category).OrderBy(b => b.Title).ToListAsync());
    public async Task<IActionResult> Details(int id) {
        var item = await db.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
        return item == null ? NotFound() : View(item);
        }
    public async Task<IActionResult> Create() { await Categories();
        return View();
        }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (book.AvailableCopies > book.TotalCopies) ModelState.AddModelError(nameof(book.AvailableCopies), "Available copies cannot exceed total copies.");
        if (!ModelState.IsValid) { await Categories();
        return View(book);
        }
        unit.Books.Add(book);
        await unit.SaveAsync();
        TempData["Success"] = "Book added successfully.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id) {
        var item = await unit.Books.GetByIdAsync(id);
        if (item == null) return NotFound();
        await Categories();
        return View(item);
        }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id) return NotFound();
        if (book.AvailableCopies > book.TotalCopies) ModelState.AddModelError(nameof(book.AvailableCopies), "Available copies cannot exceed total copies.");
        if (!ModelState.IsValid) { await Categories();
        return View(book);
        }
        unit.Books.Update(book);
        await unit.SaveAsync();
        TempData["Success"] = "Book updated successfully.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id) {
        var item = await db.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
        return item == null ? NotFound() : View(item);
        }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await unit.Books.GetByIdAsync(id);
        if (item == null) return NotFound();
        if (await db.Loans.AnyAsync(l => l.BookId == id)) { TempData["Error"] = "A book with loan history cannot be deleted.";
        return RedirectToAction(nameof(Index));
        }
        unit.Books.Delete(item);
        await unit.SaveAsync();
        TempData["Success"] = "Book deleted.";
        return RedirectToAction(nameof(Index));
    }
    private async Task Categories() => ViewBag.Categories = await db.Categories.OrderBy(c => c.Name).ToListAsync();
}
