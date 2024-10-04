using Fectum.IdentityService.Model.Models.Models;

namespace Fectum.IdentityService.AuthUser.UI.IUserInterfaces
{
    public interface IActiveUser
    {
        Task<List<UserInformation>> GetActiveUserInformationListAsync();
        Task<List<UserInformation>> GetInActiveUserInformationListAsync();
    }
}
