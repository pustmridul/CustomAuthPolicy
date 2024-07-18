using CustomAuthPolicy.Data;
using System;

namespace CustomAuthPolicy.Service
{
    public class PermissionService : IPermissionService
    {
        // Assuming you have a database context to fetch roles and permissions
        private readonly AppDbContext _context;

        public PermissionService(AppDbContext context)
        {
            _context = context;
        }

        public bool HasPermission(string role, string permission)
        {
            var rolePermissions = _context.Roles
                .Where(r => r.Name == role)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission.Name)
                .ToList();

            return rolePermissions.Contains(permission);
        }
    }
}
