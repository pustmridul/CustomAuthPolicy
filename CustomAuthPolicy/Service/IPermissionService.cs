namespace CustomAuthPolicy.Service
{
    public interface IPermissionService
    {
        bool HasPermission(string role, string permission);
    }
}
