using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using GymMasterPro.Data;

namespace GymMasterPro.Pages.Trainers
{
    public class DeleteModel : PageModel
    {
        private readonly GymMasterPro.Data.ApplicationDbContext _context;

        public DeleteModel(GymMasterPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trainers == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainers.FirstOrDefaultAsync(m => m.Id == id);

            if (trainer == null)
            {
                return NotFound();
            }
            else 
            {
                Trainer = trainer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Trainers == null)
            {
                return NotFound();
            }
            var trainer = await _context.Trainers.FindAsync(id);

            if (trainer != null)
            {
                Trainer = trainer;
                _context.Trainers.Remove(Trainer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
