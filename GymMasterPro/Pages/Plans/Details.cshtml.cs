using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using GymMasterPro.Data;

namespace GymMasterPro.Pages.Plans
{
    public class DetailsModel : PageModel
    {
        private readonly GymMasterPro.Data.ApplicationDbContext _context;

        public DetailsModel(GymMasterPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Plan Plan { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Plans == null)
            {
                return NotFound();
            }

            var plan = await _context.Plans.FirstOrDefaultAsync(m => m.Id == id);
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
