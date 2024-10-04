using Fectum.IdentityService.Model.Models.Models;

namespace Fectum.IdentityService.AuthUser.UI.IUserInterfaces
{
    public interface ILoginUser
    {
        Task<string> SignInAsync(Credentials Credentials);
    }
}
