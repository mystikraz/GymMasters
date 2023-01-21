using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities;
using GymMasterPro.Data;

namespace GymMasterPro.Pages.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly GymMasterPro.Data.ApplicationDbContext _context;

        public CreateModel(GymMasterPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FirstName");
            ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Memberships == null || Membership == null)
            {
                return Page();
            }

            _context.Memberships.Add(Membership);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
