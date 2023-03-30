using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.DAL.Entities.identityEntities;

public interface IRoleService
{
    Task AddUserToRole(AddUserToRoleRequest request);
    Task<RoleResult> CreateRole(RoleDto request);
    Task DeleteRole(RoleDto request);
    Task EditRole(string id, RoleDto request);
    Task RemoveUserFromRole(AddUserToRoleRequest request);
    Task<IEnumerable<string>> GetUserRoles(string userName);

    Task<IEnumerable<AppRole>> GetAllRoles();
    //Task<PagedResponse<RoleResponse>> GetAllRoles(RoleRequestDto request);
    //Task<IEnumerable<MenuClaimsResponse>> GetRoleClaims(string roleName);
    // Task UpdateRoleClaims(UpdateRoleClaimsDto request);
}
