using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Memberships
{
    public class DeleteModel : PageModel
    {
        private readonly IMembershipService _membershipService;

        public DeleteModel(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var membership = await _membershipService.GetById(id);

            if (membership == null)
            {
                return NotFound();
            }
            else
            {
                Membership = membership;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            await _membershipService.DeleteAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
