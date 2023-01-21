using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using GymMasterPro.Data;

namespace GymMasterPro.Pages.Checkins
{
    public class DeleteModel : PageModel
    {
        private readonly GymMasterPro.Data.ApplicationDbContext _context;

        public DeleteModel(GymMasterPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Checkin Checkin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Checkins == null)
            {
                return NotFound();
            }

            var checkin = await _context.Checkins.FirstOrDefaultAsync(m => m.Id == id);

            if (checkin == null)
            {
                return NotFound();
            }
            else 
            {
                Checkin = checkin;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Checkins == null)
            {
                return NotFound();
            }
            var checkin = await _context.Checkins.FindAsync(id);

            if (checkin != null)
            {
                Checkin = checkin;
                _context.Checkins.Remove(Checkin);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
