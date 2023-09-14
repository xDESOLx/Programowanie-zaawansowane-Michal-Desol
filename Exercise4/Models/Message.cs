using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Exercise4.Models
{
    public class Message
    {
        public Message()
        {
        }

        public Message(string senderId, string recipientId, string content)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            Content = content;
        }

        public int Id { get; set; }
        [Display(Name = "Sent at")]
        public DateTime SentAt { get; set; } = DateTime.Now;
        public string SenderId { get; set; }
        [Display(Name = "Sender")]
        public IdentityUser Sender { get; set; }
        public string RecipientId { get; set; }
        [Display(Name = "Recipient")]
        public IdentityUser Recipient { get; set; }
        [Display(Name = "Content")]
        public string Content { get; set; }
        [Display(Name = "Receipt confirmed")]
        public bool ReceiptConfirmed { get; set; } = false;
    }
}
