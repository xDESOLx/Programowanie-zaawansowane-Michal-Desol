using Exercise4.Authorization;
using Exercise4.Data;
using Exercise4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Exercise4.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public ChatHub(UserManager<IdentityUser> userManager, ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task ConfirmReceipt(int messageId)
        {
            var message = await _context.Messages!
                .Include(m => m.Sender)
                .Include(m => m.Recipient).SingleOrDefaultAsync(m => m.Id == messageId);
            if (message != null && (await _authorizationService.AuthorizeAsync(Context.User, message, new SameRecipientRequirement())).Succeeded)
            {
                if (!message.ReceiptConfirmed)
                {
                    message.ReceiptConfirmed = true;
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                    await Clients.User(message.SenderId).SendAsync("ReceiptConfirmed", message.Id);
                }
            }
        }

        public async Task<object> SendMessage(string recipientId, string content)
        {
            var sender = await _userManager.GetUserAsync(Context.User);
            var recipient = await _userManager.FindByIdAsync(recipientId);
            if (recipient is null)
            {
                throw new ArgumentException("The recipient ID is invalid.", nameof(recipientId));
            }
            var message = new Message(sender.Id, recipient.Id, content);
            _context.Add(message);
            await _context.SaveChangesAsync();
            await Clients.User(recipientId).SendAsync("ReceiveMessage", message.Id, message.Sender.UserName, message.Content, message.SentAt);
            return new
            {
                Id = message.Id,
                SentAt = message.SentAt,
                Recipient = message.Recipient.UserName,
                Content = message.Content
            };
        }
    }
}
