using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Plans
{
    public class EditModel : PageModel
    {
        private readonly IPlanService _planService;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(IPlanService planService, UserManager<IdentityUser> userManager)
        {
            _planService = planService;
            _userManager = userManager;
        }

        [BindProperty]
        public Plan Plan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var plan = await _planService.GetById(id);
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
            await _planService.UpdateAsync(Plan.Id, Plan);

            return RedirectToPage("./Index");
        }
    }
}
