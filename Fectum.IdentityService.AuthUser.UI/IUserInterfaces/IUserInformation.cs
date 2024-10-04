using Fectum.IdentityService.Model.Models.Models;

namespace Fectum.IdentityService.AuthUser.UI.IUserInterfaces
{
    public interface IUserInformation
    {
        Task<List<UserInformation>> GetUsersInformation();
        Task<UserInformation> GetUserInformationById(int Id);
    }
}
