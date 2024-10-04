using Fectum.IdentityService.AuthUser.UI.DataTransferObject;
using Fectum.IdentityService.Model.Models.Models;

namespace Fectum.IdentityService.AuthUser.UI.IUserInterfaces
{
    public interface IModifyUser
    {
        Task<UserInformation> UpdateUserInfomation(UserInformationDTO information, int Id);
        Task<UserInformation> UpdateUserInformationField(UserInformationDTO information, int Id);
    }
}
