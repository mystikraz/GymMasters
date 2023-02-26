using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Plans
{
    public class DetailsModel : PageModel
    {
        private readonly IPlanService _planService;

        public DetailsModel(IPlanService planService)
        {
            _planService = planService;
        }

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
            else 
            {
                Plan = plan;
            }
            return Page();
        }
    }
}
