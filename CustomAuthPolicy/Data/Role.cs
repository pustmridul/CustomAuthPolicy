namespace CustomAuthPolicy.Data
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
