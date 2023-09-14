using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Exercise4.Pages
{
    [Authorize]
    public class MessageHubModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public MessageHubModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public SelectList? Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Users = new SelectList(await _userManager.Users.ToListAsync(), nameof(IdentityUser.Id), nameof(IdentityUser.UserName));
            return Page();
        }
    }
}
