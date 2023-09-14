using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise4.Data;
using Exercise4.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Exercise4.Authorization;
using Exercise4.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Exercise4.Pages.History
{
    public class EditModel : PageModel
    {
        private readonly Exercise4.Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHubContext<ChatHub> _hubContext;

        public EditModel(Exercise4.Data.ApplicationDbContext context, IAuthorizationService authorizationService, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _authorizationService = authorizationService;
            _hubContext = hubContext;
        }


        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        [Required]
        public string Content { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.Include(m => m.Sender).FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            if (!(await _authorizationService.AuthorizeAsync(User, message, new SameSenderRequirement())).Succeeded)
            {
                return Forbid();
            }
            Id = message.Id;
            Content = message.Content;
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
            var message = await _context.Messages!.Include(m => m.Sender).SingleOrDefaultAsync(m => m.Id == Id);
            if (message == null)
            {
                return NotFound();
            }
            if (!(await _authorizationService.AuthorizeAsync(User, message, new SameSenderRequirement())).Succeeded)
            {
                return Forbid();
            }

            message.Content = Content;
            _context.Update(message);

            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.User(message.RecipientId).SendAsync("MessageEdited", message.Id, message.Content);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MessageExists(int id)
        {
            return (_context.Messages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
