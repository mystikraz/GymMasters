using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Trainers
{
    public class EditModel : PageModel
    {
        private readonly ITrainerService _trainerService;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(ITrainerService trainerService, UserManager<IdentityUser> userManager)
        {
            _trainerService = trainerService;
            _userManager = userManager;
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var trainer =  await _trainerService.GetById(id);
            if (trainer == null)
            {
                return NotFound();
            }
            Trainer = trainer;
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
            Trainer.UpdateAt = DateTime.Now;
            Trainer.CreatedAt = DateTime.Now;
            Trainer.CreatedBy = loggedInUser?.UserName;

            await _trainerService.UpdateAsync(Trainer.Id, Trainer);

            return RedirectToPage("./Index");
        }
    }
}
