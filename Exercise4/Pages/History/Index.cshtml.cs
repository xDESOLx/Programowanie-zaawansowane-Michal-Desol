using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Exercise4.Data;
using Exercise4.Models;
using Microsoft.AspNetCore.Identity;

namespace Exercise4.Pages.History
{
    public class IndexModel : PageModel
    {
        private readonly Exercise4.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(Exercise4.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Message> Message { get;set; } = default!;

        public async Task OnGetAsync()
        {            
            if (_context.Messages != null)
            {
                var user = await _userManager.GetUserAsync(User);
                Message = await _context.Messages
                .Include(m => m.Recipient)
                .Include(m => m.Sender)
                .Where(m => m.Sender == user).ToListAsync();
            }
        }
    }
}
