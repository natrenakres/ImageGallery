using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorization;

public class MustOwnImageHandler : AuthorizationHandler<MustOwnImageRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MustOwnImageHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnImageRequirement requirement)
    {
        var id = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

        if (!Guid.TryParse(id, out Guid imageId))
        {
            context.Fail();
            return;
        }

        var ownerId = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (ownerId == null)
        {
            context.Fail();
            return;
        }

        // Check user has requested image!
        // if (!await _IsImageOwnerAsync(imageId, ownerId))
        // {
        //     context.Fail();
        //     return;
        // }
        
        // all chesk out
        context.Succeed(requirement);

    }
}