using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymMasterPro.Pages.Members
{
    public class DeleteModel : PageModel
    {
        private readonly IMemberService _memberService;

        public DeleteModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0 || await _memberService.GetMembers() == null)
            {
                return NotFound();
            }
            var member = await _memberService.GetMemberById(id);

            if (member != null)
            {
                Member = member;
                await _memberService.DeleteAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
