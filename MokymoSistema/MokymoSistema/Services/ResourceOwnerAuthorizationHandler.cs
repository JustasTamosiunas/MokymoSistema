using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using MokymoSistema.Models;

namespace MokymoSistema.Services;

public class ResourceOwnerAuthorizationHandler : AuthorizationHandler<ResourceOwenerRequirement, IUserOwnedResouce>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOwenerRequirement requirement,
        IUserOwnedResouce resource)
    {
        if (context.User.IsInRole(UserRoles.Admin) ||
            context.User.FindFirstValue(JwtRegisteredClaimNames.Sub) == resource.UserId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public record ResourceOwenerRequirement : IAuthorizationRequirement;