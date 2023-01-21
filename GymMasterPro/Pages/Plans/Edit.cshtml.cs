using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using GymMasterPro.Data;
using Microsoft.AspNetCore.Identity;

namespace GymMasterPro.Pages.Plans
{
    public class EditModel : PageModel
    {
        private readonly GymMasterPro.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(GymMasterPro.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Plan Plan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Plans == null)
            {
                return NotFound();
            }

            var plan =  await _context.Plans.FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }
            Plan = plan;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Page();
            }
            Plan.UpdateAt = DateTime.Now;
            Plan.CreatedAt = DateTime.Now;
            Plan.CreatedBy = loggedInUser?.UserName;
            _context.Attach(Plan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(Plan.Id))
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

        private bool PlanExists(int id)
        {
          return (_context.Plans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
