using Exercise4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Exercise4.Authorization
{
    public class MessageSameRecipientAuthorizationHandler : AuthorizationHandler<SameRecipientRequirement, Message>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public MessageSameRecipientAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SameRecipientRequirement requirement, Message resource)
        {
            if (await _userManager.GetUserAsync(context.User) == resource.Recipient)
            {
                context.Succeed(requirement);
            }
        }
    }

    public class SameRecipientRequirement : IAuthorizationRequirement { }
}
