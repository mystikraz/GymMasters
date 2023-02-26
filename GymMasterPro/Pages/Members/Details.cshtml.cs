using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly IMemberService _memberService;

        public DetailsModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0 || await _memberService.GetMembers() == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            else
            {
                Member = member;
            }
            return Page();
        }
    }
}
