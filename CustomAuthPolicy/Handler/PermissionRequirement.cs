using CustomAuthPolicy.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CustomAuthPolicy.Handler
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }

    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;
        private readonly ILogger<PermissionAuthorizationHandler> _logger;

        public PermissionAuthorizationHandler(IPermissionService permissionService, ILogger<PermissionAuthorizationHandler> logger)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            _logger.LogInformation("Handling permission requirement for user: {User}", context.User.Identity.Name);

            var userRoles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            if (!userRoles.Any())
            {
                _logger.LogWarning("No roles found for user: {User}", context.User.Identity.Name);
            }

            foreach (var role in userRoles)
            {
                _logger.LogInformation("Checking permission for role: {Role}", role);

                if (_permissionService.HasPermission(role, requirement.Permission))
                {
                    _logger.LogInformation("User: {User} with role: {Role} has required permission: {Permission}", context.User.Identity.Name, role, requirement.Permission);
                    context.Succeed(requirement);
                    break;
                }
            }

            return Task.CompletedTask;
        }
    }

}
