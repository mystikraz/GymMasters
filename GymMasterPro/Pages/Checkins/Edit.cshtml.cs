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

namespace GymMasterPro.Pages.Checkins
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
        public Checkin Checkin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Checkins == null)
            {
                return NotFound();
            }

            var checkin =  await _context.Checkins.FirstOrDefaultAsync(m => m.Id == id);
            if (checkin == null)
            {
                return NotFound();
            }
            Checkin = checkin;
           ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Address");
           ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name");
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
            Checkin.UpdateAt = DateTime.Now;
            Checkin.CreatedAt = DateTime.Now;
            Checkin.CreatedBy = loggedInUser?.UserName;
            _context.Attach(Checkin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckinExists(Checkin.Id))
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

        private bool CheckinExists(int id)
        {
          return (_context.Checkins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
