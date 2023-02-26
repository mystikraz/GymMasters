using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;

namespace GymMasterPro.Pages.Members
{
    public class EditModel : PageModel
    {
        private readonly IMemberService _memberService;
        private readonly ITrainerService _trainerService;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(IMemberService memberService,
            ITrainerService trainerService,
            UserManager<IdentityUser> userManager)
        {
            _memberService = memberService;
            _trainerService = trainerService;
            _userManager = userManager;
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
            Member = member;
            var trainers = await _trainerService.GetTrainers();
            ViewData["TrainerId"] = new SelectList(trainers, "Id", "FirstName");
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
            Member.UpdateAt = DateTime.Now;
            Member.CreatedAt = DateTime.Now;
            Member.CreatedBy = loggedInUser?.UserName;

            var res = await _memberService.UpdateAsync(Member.Id, Member);
            if (res is null)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
