using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities;
using GymMasterPro.Data;
using Microsoft.AspNetCore.Identity;

namespace GymMasterPro.Pages.Checkins
{
    public class CreateModel : PageModel
    {
        private readonly GymMasterPro.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(GymMasterPro.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FirstName");
        ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Checkin Checkin { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Checkins == null || Checkin == null)
            {
                return Page();
            }
            //var loggedInUser = await _userManager.GetUserAsync(User);
            //if (loggedInUser == null)
            //{
            //    return Page();
            //}
            Checkin.UpdateAt = DateTime.Now;
            Checkin.CreatedAt = DateTime.Now;
            //Checkin.CreatedBy = loggedInUser?.UserName;
            _context.Checkins.Add(Checkin);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
