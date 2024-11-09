using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Porumb_Denisa_Lab2.Data;
using Porumb_Denisa_Lab2.Models;

namespace Porumb_Denisa_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Porumb_Denisa_Lab2.Data.Porumb_Denisa_Lab2Context _context;

        public EditModel(Porumb_Denisa_Lab2.Data.Porumb_Denisa_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;
       
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } 

            var borrowing = await _context.Borrowing.Include(b => b.Book) .ThenInclude(b => b.Author).Include(b => b.Book).ThenInclude(b => b.BookCategories).ThenInclude(bc => bc.Category).FirstOrDefaultAsync(m => m.ID == id);
            
            if (borrowing == null)
            {
                return NotFound();
            }
            Borrowing = borrowing;
          
            ViewData["BookID"] = new SelectList(await _context.Book.Include(b => b.Author).Include(b => b.BookCategories).ThenInclude(bc => bc.Category).Select(b => new { b.ID, Details = b.Author != null ? $"{b.Title} - {b.Author.FullName}" : $"{b.Title} - Unknown Author"}).ToListAsync(),"ID", "Details");
            
            ViewData["MemberID"] = new SelectList(await _context.Member.Select(m => new { m.ID, m.FullName }).ToListAsync(), "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}