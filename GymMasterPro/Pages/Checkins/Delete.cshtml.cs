using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Checkins
{
    public class DeleteModel : PageModel
    {
        private readonly ICheckinService _checkinService;

        public DeleteModel(ICheckinService checkinService)
        {
            _checkinService = checkinService;
        }

        [BindProperty]
        public Checkin Checkin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var checkin = await _checkinService.GetById(id);

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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var checkin = await _checkinService.GetById(id);

            if (checkin != null)
            {
                Checkin = checkin;
                await _checkinService.DeleteAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
