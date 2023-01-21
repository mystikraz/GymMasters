using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using GymMasterPro.Data;

namespace GymMasterPro.Pages.Checkins
{
    public class DetailsModel : PageModel
    {
        private readonly GymMasterPro.Data.ApplicationDbContext _context;

        public DetailsModel(GymMasterPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Checkin Checkin { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Checkins == null)
            {
                return NotFound();
            }

            var checkin = await _context.Checkins.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
