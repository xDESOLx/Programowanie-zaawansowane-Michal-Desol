using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Exercise4.Data;
using Exercise4.Models;
using Microsoft.AspNetCore.Authorization;
using Exercise4.Authorization;
using Microsoft.AspNetCore.SignalR;
using Exercise4.Hubs;

namespace Exercise4.Pages.History
{
    public class DeleteModel : PageModel
    {
        private readonly Exercise4.Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHubContext<ChatHub> _hubContext;

        public DeleteModel(Exercise4.Data.ApplicationDbContext context, IAuthorizationService authorizationService, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _authorizationService = authorizationService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Message Message { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

            if (message == null)
            {
                return NotFound();
            }
            else if (!(await _authorizationService.AuthorizeAsync(User, message, new SameSenderRequirement())).Succeeded)
            {
                return Forbid();
            }
            else
            {
                Message = message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }
            var message = await _context.Messages.FindAsync(id);

            if (message != null)
            {
                if (!(await _authorizationService.AuthorizeAsync(User, message, new SameSenderRequirement())).Succeeded)
                {
                    return Forbid();
                }
                await _hubContext.Clients.User(message.RecipientId).SendAsync("MessageDeleted", message.Id);
                Message = message;                
                _context.Messages.Remove(Message);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
