using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Plans
{
    public class CreateModel : PageModel
    {
        private readonly IPlanService _planService;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(IPlanService planService, UserManager<IdentityUser> userManager)
        {
            _planService = planService;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Plan Plan { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Plan == null)
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
            await _planService.SaveAsync(Plan);

            return RedirectToPage("./Index");
        }
    }
}
