﻿namespace CustomAuthPolicy.Data
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
