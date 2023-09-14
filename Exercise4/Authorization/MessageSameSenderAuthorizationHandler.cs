using Exercise4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Exercise4.Authorization
{
    public class MessageSameSenderAuthorizationHandler : AuthorizationHandler<SameSenderRequirement, Message>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public MessageSameSenderAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SameSenderRequirement requirement, Message resource)
        {
            if (await _userManager.GetUserAsync(context.User) == resource.Sender)
            {
                context.Succeed(requirement);
            }
        }
    }

    public class SameSenderRequirement : IAuthorizationRequirement { }
}
