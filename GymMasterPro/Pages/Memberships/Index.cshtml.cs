using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using GymMasterPro.Data;

namespace GymMasterPro.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly GymMasterPro.Data.ApplicationDbContext _context;

        public IndexModel(GymMasterPro.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Membership> Membership { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Memberships != null)
            {
                Membership = await _context.Memberships
                .Include(m => m.Member)
                .Include(m => m.Plan).ToListAsync();
            }
        }
    }
}
