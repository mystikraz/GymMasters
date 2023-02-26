using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Trainers
{
    public class DetailsModel : PageModel
    {
        private readonly ITrainerService _trainerService;

        public DetailsModel(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

      public Trainer Trainer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var trainer = await _trainerService.GetById(id);
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
    }
}
