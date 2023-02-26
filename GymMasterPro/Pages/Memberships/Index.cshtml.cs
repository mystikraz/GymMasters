using Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly IMembershipService _membershipService;

        public IndexModel(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public IList<Membership> Membership { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Membership = await _membershipService.GetMemberships();
        }
    }
}
