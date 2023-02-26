using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using Services.Interfaces;

namespace GymMasterPro.Pages.Checkins
{
    public class IndexModel : PageModel
    {
        private readonly ICheckinService _checkinService;

        public IndexModel(ICheckinService checkinService)
        {
            _checkinService = checkinService;
        }

        public IList<Checkin> Checkin { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Checkin = await _checkinService.GetCheckins();
        }
    }
}
